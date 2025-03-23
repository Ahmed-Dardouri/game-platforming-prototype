using UnityEngine;

public class main_char : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;       // Horizontal movement speed
    public float jumpForce = 7f;       // Force applied when jumping
    public float dashForce = 10f;      // Force applied when dashing
    private float blastForce = 30f;      // Force applied when dashing

    [Header("Ground Check Settings")]
    public float checkRadius = 0.2f;   // Radius for ground detection
    // Margin to allow a little tolerance for ground detection below the player's bottom.
    public float groundThreshold = 0.05f;

    private Rigidbody2D rb;
    private Collider2D coll;
    public bool isGrounded = false;
    private int jumpCount = 0;         // Tracks jumps for double jump (0 means ready to jump)
    private bool hasDashed = false;    // Ensures dash is used only once per jump

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        rb.freezeRotation = true; // Lock player rotation
    }

    void Update()
    {
        // Calculate the bottom center of the player's collider.
        Vector2 bottomCenter = new Vector2(coll.bounds.center.x, coll.bounds.min.y);

        // Use OverlapCircleAll in case multiple colliders are hit.
        Collider2D[] hits = Physics2D.OverlapCircleAll(bottomCenter, checkRadius);
        isGrounded = false;
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("ground"))
            {
                // Get the point on the collider closest to bottomCenter.
                Vector2 closestPoint = hit.ClosestPoint(bottomCenter);
                // If the closest point is at or just below our bottom center (within a small threshold),
                // then consider the player grounded.
                if (closestPoint.y <= bottomCenter.y + groundThreshold)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Reset jump and dash when grounded.
        if (isGrounded)
        {
            jumpCount = 0;
            hasDashed = false;
        }

        // Horizontal Movement.
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Jump and Double Jump.
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || jumpCount < 1)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                jumpCount++;
            }
        }

        // Dash (only once per jump).
        if (Input.GetKeyDown(KeyCode.LeftShift) && !hasDashed)
        {
            Vector2 dashDirection = new Vector2(moveInput, 0f);
            if (dashDirection == Vector2.zero)
            {
                dashDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
            }
            rb.AddForce(dashDirection.normalized * dashForce, ForceMode2D.Impulse);
            hasDashed = true;
        }



        if (Input.GetKeyDown("r")){
            Debug.Log("blast!");
            rb.linearVelocity = new Vector2(blastForce, 0f);
            Debug.Log ("vector: " + rb.linearVelocity);
        }

    }

    // Optional: Visualize the ground check area in the Unity Editor.
    void OnDrawGizmosSelected()
    {
        if (coll != null)
        {
            Vector2 bottomCenter = new Vector2(coll.bounds.center.x, coll.bounds.min.y);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(bottomCenter, checkRadius);
        }
    }
}
using UnityEngine;

public class blastManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [Header("Blast")]
    [SerializeField] private float blastForce = 20f;
    private Vector2 forceDirection = new Vector2(0, 0);
    private int damage = 30; /* must be positive */

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collision object tag: " + other.tag);
        // Check if the object has the "isMovable" tag
        if (other.CompareTag("isMovable")){
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null){
                // Calculate direction away from the blast center
                forceDirection.y = transform.right.x;
                forceDirection.x = transform.right.y * -1;
                    
                rb.linearVelocity = forceDirection * blastForce;
                Debug.Log("rb linear velocity : " + rb.linearVelocity);
            }
        }else if(other.CompareTag("isEnemie")){
            EnemyStatManager enemy_stat = other.GetComponent<EnemyStatManager>();
            enemy_stat.ApplyDamage(damage);
        }
    }
}

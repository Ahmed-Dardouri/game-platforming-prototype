using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;     // The player to follow (assign in Inspector)
    public Vector3 offset = new Vector3(0f, 2f, -18f);  // Default offset so the camera stays behind the player
    public float smoothing = 650f; // How smoothly the camera follows the player

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the target position based on the player's position plus the offset
            Vector3 targetPosition = target.position + offset;
            // Smoothly interpolate between the current position and the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }
}

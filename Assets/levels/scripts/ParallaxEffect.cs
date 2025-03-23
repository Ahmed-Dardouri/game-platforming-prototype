using UnityEngine;
using System.Collections.Generic;

public class ParallaxEffect : MonoBehaviour
{
    [Header("Parallax Settings")]
    public float smoothing = 1f; // How smoothly the layers move

    public Vector3 deltaMovement = new Vector3();
    private float ParallaxEffect_multiplier = 3f;
    private Transform cam;
    private Vector3 previousCamPos;
    

    // Lists to hold the parallax layers and their corresponding parallax factors.
    private List<Transform> planes = new List<Transform>();
    private List<float> parallaxFactors = new List<float>();
    private List<Vector3> prev_plane_pos = new List<Vector3>();


    void Start()
    {
        // Get the main camera's transform
        cam = Camera.main.transform;
        previousCamPos = cam.position;

        // Loop through each child of the background object.
        // You can customize the criteria here (e.g., by tag or name).
        foreach (Transform child in transform)
        {
            // In this example we assume child objects with "Plane" in their name are our layers.
            if (child.name.Contains("Plane"))
            {
                planes.Add(child);
                // Calculate a parallax factor based on the absolute difference between the camera's z and the child's z.
                // Larger differences yield a higher factor (resulting in a slower movement).
                float factor = Mathf.Abs(cam.position.z - child.position.z);
                // If factor is 0 (layer at the same z as the camera), default to 1 to avoid division by zero.
                if (factor == 0) factor = 1f;
                parallaxFactors.Add(factor);
                prev_plane_pos.Add(child.position);
                   // Debug.Log(child.name + " factor: " + factor);
            }
        }
    }

    void LateUpdate()
    {
        // Calculate how much the camera has moved since the last frame.
        deltaMovement = cam.position - previousCamPos;
        // Debug.Log(" deltaMovement: " + deltaMovement);

        // Apply parallax to each plane.
        for (int i = 0; i < planes.Count; i++)
        {
            // The parallax effect is achieved by dividing the camera's movement by the parallax factor.
            // This means layers further away (with a higher factor) will move less.
            Vector3 relative_pos_to_camera = deltaMovement - new Vector3(deltaMovement.x / parallaxFactors[i], deltaMovement.y / parallaxFactors[i], 0) * ParallaxEffect_multiplier;
            relative_pos_to_camera.z = 0;
            // Smoothly move the plane to its target position.
            planes[i].position += relative_pos_to_camera;
            if(prev_plane_pos[i] != planes[i].position){
                
                // Debug.Log("Plane " + i + " rel pos: " + relative_pos_to_camera.x + " " + relative_pos_to_camera.y + " " + relative_pos_to_camera.z);
            }
            prev_plane_pos[i] = planes[i].position;
        }

        // Update the previous camera position.
        previousCamPos = cam.position;
        
    }
}

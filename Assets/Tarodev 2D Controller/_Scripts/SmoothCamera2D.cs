using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {
	
	public float dampTime = 0.2f;
	private Vector3 velocity = Vector3.zero;
	public GameObject target;
    public Vector3 const_delta = new Vector3(0, 2, -10f);
    public float multiplier = 0.16f;
    private Rigidbody2D target_rb;

	// Update is called once per frame
	void FixedUpdate() 
	{
		if (target)
		{
            target_rb = target.GetComponent<Rigidbody2D>();
            Vector3 delta = const_delta;
            delta.y += target_rb.linearVelocityY * multiplier;
			Vector3 destination = target.transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	
	}
}
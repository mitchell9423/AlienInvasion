using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn : MonoBehaviour 
{
	private Rigidbody rb;
	public KeyCode Left, Right;
	public float speed;

	// Load Rigidbody from attatched gameobject
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	//key binding for camera rotation
	void FixedUpdate()
	{
		float turn = 0.0f;

		if (Input.GetKey (Left))
			turn = speed * -1;

		if (Input.GetKey (Right))
			turn = speed;
		
		Vector3 direction = new Vector3 (0.0f, turn, 0.0f);

		rb.transform.Rotate(direction);
	}
}

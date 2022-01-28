using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour {

	// Use this for initialization
	private int count =1;
	private int direction = 1;
	public char axis;

	// assigns correct starting postion based on enemy location
	void Start () {
		Vector3 setPosition = transform.position;

		if (axis == 'z')
		{
		setPosition.z = 50;
			if (transform.position.z < 0) 
			{
				setPosition.z = -60;
			}
		} 
		else 
		{
		setPosition.x = 50;
			if (transform.position.x < 0) 
			{
				setPosition.x = -60;
			}
		}
		transform.position = setPosition;
	}
	
	// determines and executes proper axis of movement
	void Update ()
	{
		transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}

	void FixedUpdate()
	{
		if (axis == 'x') 
		{
			Vector3 movement = transform.position;

			if (count == 100) 
			{
				direction *= -1;
				count = 1;
			} 

			movement.x += direction * .1f;
			transform.position = movement;
			count++;
		}

		if (axis == 'z') 
		{
			Vector3 movement = transform.position;

			if (count == 100) 
			{
				direction *= -1;
				count = 1;
			} 

			movement.z += direction * .1f;
			transform.position = movement;
			count++;
		}
	}
}

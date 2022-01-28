using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collisions : MonoBehaviour
{
	
	//public Text winText,score;
	//public float winTime = 0.0f;
	//private float timeRemaining = 5f;
	//public int lap = 1;
	//private Vector3 startPosition = new Vector3 ();
	////private Movement booster;
	////public AudioClip sound;


	//// Use this for initialization
	//void Start () 
	//{
	//	startPosition = transform.position;
	//	booster = GetComponent<Movement> ();
	//}
	
	//// displays current score
	//void Update()
	//{
	//	if (lap <= 3) 
	//	{
	//		score.text = "Lap: " + lap.ToString ();
	//	}
	//	textTimer ();
	//}

	//// general event timer
	//public void textTimer()
	//{
	//	timeRemaining -= Time.deltaTime;
	//	if (timeRemaining <= 0.0f) 
	//	{
	//		timeRemaining = 5;
	//		//booster.speed = 10;
	//	}

	//}

	//// handles collisions
	//void OnTriggerEnter(Collider other)
	//{
	//	if (other.gameObject.CompareTag("restart"))
	//	{
	//		resetPlayer ();
	//	}

	//	if (other.gameObject.CompareTag("boost"))
	//	{
	//		boostPlayer();
	//	}

	//	if (other.gameObject.CompareTag ("Finish")) 
	//	{
	//		lap++;
	//		winTime = Time.realtimeSinceStartup;
	//	}
	//}

	//// moves player to starting point
	//void resetPlayer ()
	//{
	//	transform.position = startPosition;
	//}

	//// controls boosting effects
	//void boostPlayer()
	//{
	//	timeRemaining = 0.55f;
	//	//booster.speed = 35;
	//}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class notifications : MonoBehaviour {

	public Text playerOne, playerTwo;
	public Transform buttons;
	public collisions player1, player2;

	void Start () {
		
	}

	// controls messages on canvas
	void Update () 
	{
		//if ((player1.lap > 3) || (player2.lap > 3))
		//{
		//	if (player1.winTime > player2.winTime) 
		//	{
		//		playerOne.text = "Winner!";
		//		playerTwo.text = "You Lose!";
		//	} 
		//	else 
		//	{
		//		playerOne.text = "You Lose!";
		//		playerTwo.text = "Winner!";
		//	}
		//	buttons.gameObject.SetActive (true);
		//}
	}
}

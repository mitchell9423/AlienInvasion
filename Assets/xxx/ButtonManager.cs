using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ButtonManager : MonoBehaviour {

    //public Transform buttons, info;
    public GameObject escapeMenu;
	public AudioClip soundOne, soundTwo;
    public static bool newHighScore;

    public void Start()
    {
        newHighScore = false;
    }

    // Opens menu when esc is pressed
    void Update () 
	{
    }
}

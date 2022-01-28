using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{
	public Text winText;
    public Text scoreText;
    public GameObject player;
	public GameObject player2;

    private void Start()
    {
        winText.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        winText.text = "Score!";
        StartCoroutine(textWait());        
    }

    IEnumerator textWait()
    {
        yield return new WaitForSecondsRealtime(2);
        player.transform.position = new Vector3(1, 1, 3);
		player2.transform.position = new Vector3 (1, 1, 1);
        winText.text = "";
    }


}

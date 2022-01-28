using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour {

	public AudioClip sound;

	// plays audioclip of attached object on collision
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			GetComponent<AudioSource> ().PlayOneShot (sound);
		}
	}
}

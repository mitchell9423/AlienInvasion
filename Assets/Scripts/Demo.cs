using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Demo : MonoBehaviour {
    public Animator animate;
    public ParticleSystem flash;
    public ParticleSystem mflash;
    public ParticleSystem[] explosion;
    public Transform weapon1;
    public Transform mortor;
    public AudioSource shotSound;

    bool running = false;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x == 0.0 && !running)
        {
            StartCoroutine(Fire_Weapon());
            running = true;
        }
    }

    IEnumerator Fire_Weapon()
    {
        yield return new WaitForSeconds(1.5f);

        flash.Play();
        GameObject handler = Instantiate(Resources.Load("Bullet"), weapon1.position, weapon1.rotation) as GameObject;
        Transform tr = handler.transform;
        tr.Translate(0.0f, 1.0f, 0.0f);
        Rigidbody temp;
        temp = handler.GetComponent<Rigidbody>();
        temp.AddForce(weapon1.up * 1000);
        Destroy(handler, 2.0f);

        yield return new WaitForSeconds(.2f);
        foreach (ParticleSystem boom in explosion)
        {
            boom.Play();
        }

        yield return new WaitForSeconds(4.0f);
        animate.SetTrigger("Fire");
        Invoke("fire_weapon", .35f);
        
        running = false;
    }

    public void fire_weapon()
    {
        mflash.Play();
        GameObject handler = Instantiate(Resources.Load("Mortor"), mortor.position, mortor.rotation) as GameObject;

        Transform tr = handler.transform;
        tr.Translate(0.0f, 1.6f, 0.0f);

        Rigidbody temp;
        temp = handler.GetComponent<Rigidbody>();
        temp.AddForce(mortor.up * 800);
        Destroy(handler, 1.0f);
        //timeRemaining = 2.6f;
        //shotSound.pitch = .38f;
        //shotSound.Play();
    }
}

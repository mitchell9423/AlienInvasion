using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Two : Weapons
{
    private float timeRemaining;
    private Transform[] cannon = new Transform[2];
    public ParticleSystem[] flash;



    protected override void Start()
    {
        offsetPos = new Vector3(0.0f, 0.0f, 0.4f);
        offsetRot = new Vector3(0.0f, 0.0f, 0.0f);
        cannon[0] = GameObject.Find("Weapon_Two_A").GetComponent<Transform>();
        cannon[1] = GameObject.Find("Weapon_Two_B").GetComponent<Transform>();
        shotSound = GetComponent<AudioSource>();
    }


    
    public override void position()
    {
        transform.eulerAngles = playerT.eulerAngles + offsetRot;
        transform.position = playerT.position;
        transform.Translate(offsetPos);
    }

    public override void shoot()
    {
        timeRemaining -= Time.deltaTime;

        if (gameObject.activeSelf)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (timer <= 0.0f)
                {
                    foreach (ParticleSystem p in flash)
                        p.Play();

                    foreach (Transform cn in cannon)
                    {
                        GameObject handler = Instantiate(Resources.Load("Bullet"), cn.position, cn.rotation) as GameObject;

                        Rigidbody temp;
                        temp = handler.GetComponent<Rigidbody>();
                        temp.AddForce(transform.forward * 1000);
                        Destroy(handler, 2f);
                        playerScript.cap -= playerScript.cost;
                        Transform tr = handler.transform;
                        tr.Translate(0.0f, 1.6f, 0.0f);
                    }

                    shotSound.pitch = .38f;
                    shotSound.Play();
                    timer = (playerScript.fireRate / save.fireRateMod)/1.5f;
                }
                timer -= Time.deltaTime;
            }
        }
    }
}

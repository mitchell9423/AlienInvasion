using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_One : Weapons
{
    [SerializeField] ParticleSystem flash;
    [SerializeField] ParticleSystem bullets;

    protected override void Start()
    {
        offsetPos = new Vector3(0.7f, 0.25f, 0.4f);
        offsetRot = new Vector3 (90, -1, 0.0f);
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
        if (gameObject.activeSelf)
        {
            if (timer <= 0.0)
            {
                if (Input.GetKey(KeyCode.Mouse0) && playerScript.cap > 0.0f)
                {
                    if (flash) flash.Play();
                    shotSound.Play();
                    //if (bullets) bullets.Play();
					//GameObject handler = Instantiate(Resources.Load("Bullet"), transform.position, transform.rotation) as GameObject;

					//Transform tr = handler.transform;
					//tr.Translate(0.0f, 1.0f, 0.0f);

					//Rigidbody temp;
					//temp = handler.GetComponent<Rigidbody>();
					//temp.AddForce(transform.up * 1000);

					//Destroy(handler, 2.0f);
					playerScript.cap -= playerScript.cost;
                    timer = playerScript.fireRate / save.fireRateMod;
                }
            }
            timer -= Time.deltaTime;
        }
    }
}

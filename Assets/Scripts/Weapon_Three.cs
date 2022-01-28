using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Three : Weapons
{
    private float timeRemaining;
    public static int shells;
    public ParticleSystem flash;
    public Animator recoil;
    public Transform mortor;
    Transform start;

    protected override void Start() //assigns Vector3's to offset variables, declares audio
    {
        offsetPos = new Vector3(-0.7f, 0.0f, 0.25f);
        offsetRot = new Vector3(90.0f, 0.0f, 0.0f);
        shotSound = GetComponent<AudioSource>();
        shells = 3;
    }

    public override void position()
    {
        transform.eulerAngles = playerT.eulerAngles + offsetRot;
        transform.position = playerT.position;
        transform.Translate(offsetPos);
    }

    //fires a mortor the explodes in the air
    public override void shoot()
    {
        timeRemaining -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse1) && (!Weapon_Manager.overdrive))
        {
            if (timeRemaining < 0)
            {
                if (shells > 0)
                {
                    shells--;
                    recoil.SetTrigger("Fire");
                    Invoke("fire_weapon", .35f);
                }
            }
        }
    }

    public void fire_weapon()
    {
        flash.Play();
        GameObject handler = Instantiate(Resources.Load("Mortor"), mortor.position, mortor.rotation) as GameObject;

        Transform tr = handler.transform;
        tr.Translate(0.0f, 1.6f, 0.0f);

        Rigidbody temp;
        temp = handler.GetComponent<Rigidbody>();
        temp.AddForce(mortor.up * 800);
        timeRemaining = 2.6f;
        shotSound.pitch = .38f;
        shotSound.Play();
    }
}

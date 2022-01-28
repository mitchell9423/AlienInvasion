using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour {
    float timeRemaining, fireRate;
    //public Transform[] cannon;
    public Transform cannon;
    public AudioSource shotSound;
    //public ParticleSystem[] flash;
    public ParticleSystem flash;

    public Animation recoil;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    //    public override void shoot()
    //{
        //if (playerS.FirstShot)
        //{
        //    reload();
        //    timeRemaining = 0;
        //    fireRate = 0;
        //    playerS.FirstShot = false;
        //}

        timeRemaining -= Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (gameObject.activeSelf)
            {
                fireRate -= Time.deltaTime;

                if ((fireRate < 0) && (timeRemaining < 0))
                {
                    //foreach (ParticleSystem p in flash)
                    //p.Play();
                    //recoil.Play("Recoil");
                    recoil.Play();
                    flash.Play();

                    //if (shots > 0)
                    //{
                    foreach (Transform cn in cannon)
                        {
                            GameObject handler = Instantiate(Resources.Load("tBullet"), cn.position, cn.rotation) as GameObject;

                            Rigidbody temp;
                            temp = handler.GetComponent<Rigidbody>();
                            temp.AddForce(transform.forward * 1000);
                            Destroy(handler, 2f);

                            Transform tr = handler.transform;
                            tr.Translate(0.0f, 1.6f, 0.0f);
                        }
                    
                    shotSound.pitch = .38f;
                    shotSound.Play();
                    //shots--;
                    //}
                    fireRate = .1f;
                }
                
            }

            //if (shots < 1)
            //{
            //    reload();
            //    timeRemaining = 2;
            //}

            //if (timeRemaining < -1.5f)
            //{
            //    reload();
            //    timeRemaining = 0;
            //}
        }
    }
//}
}

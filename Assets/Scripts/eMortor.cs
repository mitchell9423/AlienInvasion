using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eMortor : MonoBehaviour
{

    public ParticleSystem particle;
    public MeshRenderer render;
    public AudioSource sound;
    Player player;

    public float radius;
    public float power;
    public float upforce;
    public SphereCollider col;

    bool detonated = false;

    // Use this for initialization
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    //proximity detonator for enemy mortors
    public void OnTriggerEnter(Collider other)
    {
        if (!detonated)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hit in colliders)
            {
                if (hit.gameObject.CompareTag("Player"))
                {
                    player.playerHealth -= 2.5f;
                    Save_Script.streak = 1;
                }

                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                    rb.AddExplosionForce(power, transform.position, radius, upforce, ForceMode.Impulse);
            }
            render.enabled = false;
            particle.Play();
            sound.Play();
            Destroy(gameObject, 2.0f);
        }

        detonated = true;
    }


}

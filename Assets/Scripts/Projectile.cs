using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //private Transform thisTransform;
    public AudioClip sound;
    private Vector3 startPosition;
    private SphereCollider col;
    protected static GameObject bullet, ebullet;
    private float timeRemaining = .8f, lifeSpan = 10;
    //public ParticleSystem particle;
    public MeshRenderer render;
    public Animator boom;

    public float radius;
    public float power;
    public float upforce;
    Enemies escript;

    protected void Start()
    {
        bullet = Resources.Load("Bullet") as GameObject;
        ebullet = Resources.Load("eBullet") as GameObject;
        //thisTransform = transform;
        startPosition = transform.position;
        col = GetComponent<SphereCollider>();
    }

    protected void Update()
    {
        timeRemaining -= Time.deltaTime;
        delay();
        life();
    }

    public void life()
    {
        if (lifeSpan <= 0.0f)
            Destroy(gameObject);
        lifeSpan -= Time.deltaTime;
    }
   
    public virtual void delay() //delays activation of projectile collider after instantiation
    {
        float distance = Vector3.Distance(startPosition, transform.position);
        if (distance < 1)
        {
            col.enabled = false;
        }
        else
        {
            col.enabled = true;
        }
    }

    //proximity detonator for enemy mortors
    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("bullet"))
        {
            if (!other.gameObject.CompareTag("eBullet"))
            {
                //AudioSource.PlayClipAtPoint(sound, transform.position, 100.0f);
                Destroy(gameObject);
            }

        }
        
    }
}

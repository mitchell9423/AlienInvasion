using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyThree : Enemies
{
    public Transform turret;
    public Transform muzzle;
    public Transform found;
    public Animator animate;
    public Rigidbody head;
    public Rigidbody bottom;
    public ParticleSystem deathflame;
    public ParticleSystem deathbang;
    public AudioSource startDeath;
    public AudioClip[] deathSounds;
    public GameObject minibubble;

    public float thrust;
    public float multiplier;

    public ParticleSystem flash;
    public ParticleSystem rt;
    public AudioSource rs;
    public ParticleSystem lt;
    public AudioSource ls;
    public ParticleSystem bt;
    public AudioSource bs;
    public ParticleSystem ft;
    public AudioSource fs;
    public AudioSource lft;
    ParticleSystem.MainModule mainbt;
    ParticleSystem.MainModule mainlt;
    ParticleSystem.MainModule mainrt;
    ParticleSystem.MainModule mainft;
    
    float timer;

    public override void Start()
    {
        head.angularDrag = 0.0f;
        mainbt = bt.main;
        mainlt = lt.main;
        mainrt = rt.main;
        mainft = ft.main;
        agent = this.GetComponent<NavMeshAgent>();
        health = 20 * SceneManager.GetActiveScene().buildIndex;
        base.Start();
    }
    public override void Update()
    {
        move();

        if (!isdead && !Player.isDead)
            shoot();

        if (health <= 0 && !isdead)
        {
                startDeath.PlayOneShot(deathSounds[Random.Range(2,4)]);
                Invoke("explosion", 4.0f);
        }
        die();
    }

    public override void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("bullet"))
                health--;
    }

    public void die()
    {
        if (health <= 0)
        {
            if (!isdead)
            {
                isdead = true;
                controller.EnemiesDestroyed++;
                dropLoot();
                Save_Script.score_keeper();
            }

            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                bs.volume -= .02f;
                ls.volume -= .02f;
                rs.volume -= .02f;
                fs.volume -= .02f;
                lft.volume -= .02f;
            }
        }
    }


    public override void shoot()
    {
        //rv = target.position;
        //rv.y = 0.0f;
        //turret.LookAt(rv);

        Vector3 relativePos = (target.position + new Vector3(0, -1.0f, 0)) - turret.position;
        turret.rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
        turret.position = found.position;
        if (agent.remainingDistance <= agent.stoppingDistance + 10.0f)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining < 0)
            {
                GameObject handler = Instantiate(Resources.Load("eMortor"), muzzle.position, muzzle.rotation) as GameObject;

                Transform tr = handler.transform;
                tr.Rotate(0.0f, 0.0f, 0.0f);
                tr.Translate(0.0f, 0.0f, .5f);

                Rigidbody temp;
                temp = handler.GetComponent<Rigidbody>();
                temp.AddForce(handler.transform.forward * 2000);
                Destroy(handler, 5.0f);
                timeRemaining = 5.0f;
                animate.SetTrigger("Shot");
                flash.Play();
                startDeath.PlayOneShot(deathSounds[4]);
            }
        }
    }

    public override void move()
    {
        if (!Player.isDead && !isdead)
            base.move();
        else
            agent.SetDestination(transform.position);

        if (!isdead)
        {
            if (agent.velocity.z > 0.5f)
            {
                mainbt.startSpeed = ((agent.velocity.z + 1.0f) * multiplier) * thrust;
                bt.Play();
                if (!bs.isPlaying)
                    bs.Play();
            }
            else
            {
                bt.Stop();
                bs.Stop();
            }


            if (agent.velocity.x > 0.5f)
            {
                mainlt.startSpeed = ((agent.velocity.x + 1.0f) * multiplier) * thrust;
                lt.Play();
                if (!ls.isPlaying)
                    ls.Play();
            }
            else
            {
                lt.Stop();
                ls.Stop();
            }

            if (agent.velocity.x < -0.5f)
            {
                mainrt.startSpeed = ((agent.velocity.x - 1.0f) * multiplier) * -thrust;
                rt.Play();
                if (!rs.isPlaying)
                    rs.Play();
            }
            else
            {
                rt.Stop();
                rs.Stop();
            }

            if (agent.velocity.z < -0.5f)
            {
                mainft.startSpeed = ((agent.velocity.z - 1.0f) * multiplier) * -thrust;
                ft.Play();
                if (!fs.isPlaying)
                    fs.Play();
            }
            else
            {
                ft.Stop();
                fs.Stop();
            }
        }
    }

    public void explosion()
    {
        timer = 7.0f;
        head.constraints = RigidbodyConstraints.None | RigidbodyConstraints.None;
        head.AddExplosionForce(50.0f, transform.position, radius, 1.0f, ForceMode.Impulse);
        head.useGravity = true;
        bt.Play();
        bs.enabled = false;
        lt.Play();
        ls.enabled = false;
        rt.Play();
        rs.enabled = false;
        ft.Play();
        fs.enabled = false;
        bottom.useGravity = true;
        agent.baseOffset = 0.0f;
        agent.speed = 0.0f;
        deathbang.Play();
        deathflame.Play();
        startDeath.PlayOneShot(deathSounds[0]);
        startDeath.PlayOneShot(deathSounds[1]);
        Destroy(gameObject, 7.0f);
        minibubble.SetActive(false);
    }
}


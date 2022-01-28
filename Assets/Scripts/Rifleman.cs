using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Rifleman : Enemies
{

    public AudioSource deathPlayer;
    public AudioClip[] deathClips;
    public GameObject minibubble;

    public override void Start()
    {
        weaponAudio = this.GetComponent<AudioSource>();
        animator = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = Random.Range(10, 15);
        rifle = this.gameObject.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(4).GetChild(0).gameObject;
        weapon = rifle.GetComponent<Transform>();
        health = 5 * SceneManager.GetActiveScene().buildIndex;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        die();
    }

    public override void shoot()
    {
        if (!Player.isDead)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
                actions.Aiming();
            base.shoot();
        }
        else
            actions.Stay();
    }

    public override void move()
    {
        if (!Player.isDead)
            base.move();
        else
            agent.SetDestination(transform.position);

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            actions.Run();
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
            health--;
        die();
    }

    public void die()
    {
        if (!isdead)
        {
            if (health <= 0)
            {
                dropLoot();
                Save_Script.score_keeper();
                isdead = true;
                actions.Death();
                deathPlayer.PlayOneShot(deathClips[Random.Range(0, 4)]);
                Destroy(gameObject, 5.0f);
                minibubble.SetActive(false);
            }
        }
    }
}
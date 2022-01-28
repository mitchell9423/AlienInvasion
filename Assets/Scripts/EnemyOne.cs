using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyOne : Enemies
{
    Player player;
    float timer = 1.0f;
    float topSpeed, minSpeed;
    public ParticleSystem particle;
    Emmissions emmissions;
    ParticleSystem.EmissionModule psMain;
    public AudioSource[] explosions;
    public GameObject minibubble;

    public override void Start()
    {
        psMain = particle.emission;
        weaponAudio = this.GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = this.gameObject.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        agent.speed *= (float)(SceneManager.GetActiveScene().buildIndex) / 2.0f;
        topSpeed = agent.speed;
        minSpeed = topSpeed / 2.0f;
        health = 1 * SceneManager.GetActiveScene().buildIndex;
        base.Start();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (!isdead)
        {
            if (other.gameObject.CompareTag("bullet"))
                health--;

            if (health <= 0)
            {
                Save_Script.score_keeper();
                controller.EnemiesDestroyed++;
                dropLoot();
                explosion();
            }
        }
    }

    public void explosion()
    {
        isdead = true;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        animator.SetTrigger("Death");
        foreach (Collider hit in colliders)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                player.playerHealth -= 5;
                Save_Script.streak = 1;
            }

            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(power, transform.position, radius, upforce, ForceMode.Impulse);
        }
        foreach (AudioSource boom in explosions)
            boom.Play();
        minibubble.SetActive(false);
        Destroy(gameObject, 3.0f);
    }

    public override void shoot()
    {
    }

    void glow()
    {
        if (!Player.isDead)
        {
            if (agent.remainingDistance < 28.0f)
                animator.SetBool("Activated", true);
            if (agent.remainingDistance > 21.0f)
                animator.speed = 1.0f;
            else if (agent.remainingDistance > 14.0f)
            {
                animator.speed = 2.0f;
            }

            if (agent.remainingDistance < 14.0f)
            {
                psMain.rateOverTime = 80.0f;
                animator.speed = 3.0f;
                agent.speed = minSpeed;
            }
            else
                agent.speed = topSpeed;

            distance = Vector3.Distance(transform.position, target.position);

            if (distance < 5.0f || health <= 0)
            {
                explosion();
            }
        }
        else
        {
            animator.SetBool("Idle", true);
        }
    }

    public override void move()
    {
        if (!isdead)
        {

            if (!Player.isDead)
                base.move();
            else
                agent.SetDestination(transform.position);

            timer -= Time.deltaTime;
            if (timer <= 0.0f)
                glow();
        }
    }
}

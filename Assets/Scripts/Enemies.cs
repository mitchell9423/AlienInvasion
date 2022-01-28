using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    protected bool isdead = false;
    protected static float radius = 5.0f, power = 10, upforce =0.0f;
    public static Vector3 rv = new Vector3();
    protected Actions actions;
    protected float timeRemaining = 1.0f;
    protected GameObject temp;
    protected static Transform target;
    protected static GameObject[] lootArry;
    protected static Controller controller;
    protected NavMeshPath path;
    public int health;
    protected float distance;
    protected float stop;
    protected GameObject rifle;
    protected float norm;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected AudioSource weaponAudio;
    protected Transform weapon;

    public void Awake()
    {
    }

    public virtual void Start()
    {
        actions = GetComponent<Actions>();
        stop = Random.Range(2.5f, 10);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        controller = GameObject.Find("Controller").GetComponent<Controller>();
        agent = GetComponent<NavMeshAgent>();
        norm = agent.speed;
        laodLoot();

        if (agent.path.status == NavMeshPathStatus.PathComplete)
            agent.SetDestination(target.position);
    }

   public virtual void Update()
    {
        if (!isdead)
        {
            move();
            shoot();
        }
    }

    public virtual void shoot()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            timeRemaining -= Time.deltaTime;
            Vector3 relativePos = (target.position + new Vector3(0,-1.7f,0)) - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos, new Vector3(0,1,0));

            if (timeRemaining < 0)
            {
                GameObject handler = Instantiate(Resources.Load("eBullet"), weapon.position, weapon.rotation) as GameObject;

                Transform tr = handler.transform;
                tr.Rotate(90.0f, 0.0f, 0.0f);
                tr.Translate(0.0f, 1.0f, 0.0f);

                Rigidbody temp;
                temp = handler.GetComponent<Rigidbody>();
                temp.AddForce(handler.transform.up * 150);
                Destroy(handler, 5.0f);
                timeRemaining = 3f;
                weaponAudio.Play();
                actions.Attack();
            }
        }
    }


    public virtual void OnTriggerEnter(Collider other)
    {
        if (!isdead)
        {
            if (other.gameObject.CompareTag("bullet"))
                health--;

            if (health <= 0)
            {
                dropLoot();
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

                foreach (Collider hit in colliders)
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                        rb.AddExplosionForce(power, transform.position, radius, upforce, ForceMode.Impulse);
                }

                Destroy(gameObject);
            }
        }
    }

    private void laodLoot()
    {
        lootArry = new GameObject[3];
        lootArry[0] = Resources.Load("sBattery") as GameObject;
        lootArry[1] = Resources.Load("Shells") as GameObject;
        lootArry[2] = Resources.Load("mBattery") as GameObject;
    }

    public void dropLoot()
    {
        int luck = Random.Range(1, 10);

        if (luck == 3)
        {
            int loot = Random.Range(0, 3);
            Quaternion prefabRotation = lootArry[loot].transform.rotation;
            temp = Instantiate(lootArry[loot], transform.position, prefabRotation) as GameObject;
        }
    }

    public virtual void move()
    {
        if (!Player.isDead)
            if (agent.path.status == NavMeshPathStatus.PathComplete)
                agent.SetDestination(target.position);
        else
                agent.SetDestination(transform.position);
    }
}

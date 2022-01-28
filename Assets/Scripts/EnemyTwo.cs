using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTwo : Enemies
{
    public float duration;

    public override void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        health = 10;
        duration = Random.Range(-10, 0);
        base.Start();
    }

    public override void shoot()  //enemy will shoot mortors that explode on contact
    {
        if (duration < 3)
        {
            float distance;

            timeRemaining -= Time.deltaTime;

            distance = Vector3.Distance(transform.position, target.position);

            if (distance <= 50)
            {
                if (timeRemaining < 0)
                {
                    transform.Rotate(10f, 0.0f, 0.0f);
                    GameObject handler = Instantiate(Resources.Load("eMortor"), transform.position, transform.rotation) as GameObject;

                    Transform tr = handler.transform;
                    tr.Translate(0.0f, 1.6f, 0.0f);

                    Rigidbody temp;
                    temp = handler.GetComponent<Rigidbody>();
                    temp.AddForce(transform.up * 1000);
                    timeRemaining = 4;
                }
            }
        }
    }
}

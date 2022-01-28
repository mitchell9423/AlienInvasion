using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
   public int speed;
   
   void Update()
    {
        transform.position = new Vector3((Mathf.PingPong(Time.time * speed, 22) + 5), 1.01f, 2);
    }
}

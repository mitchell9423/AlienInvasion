using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyTwo : MonoBehaviour
{
    public int speed;

    void Update()
    {
        transform.position = new Vector3(2, 1.01f, (Mathf.PingPong(Time.time * speed, 24) + 7));
    }

}


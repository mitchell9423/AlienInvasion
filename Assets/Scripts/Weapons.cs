using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public static GameObject player;
    public static Transform playerT;
    public static Weapon_Manager playerS;
    public static Player playerScript;
    public static Save_Script save;
    protected Vector3 offsetPos;
    protected Vector3 offsetRot;
    protected AudioSource shotSound;
    protected float timer;

    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerT = player.GetComponent<Transform>();
        playerS = player.GetComponent<Weapon_Manager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        save = Save_Script.Instance;
    }

    public virtual void shoot()
    { }

    public virtual void position()
    {
    }
}

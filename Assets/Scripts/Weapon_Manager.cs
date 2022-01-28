using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weapon_Manager : MonoBehaviour
{
    public AudioClip[] sound;
    public Weapons[] weaponBase;
    private List<Weapons> weaponList = new List<Weapons>();
    public static int mode;
    public static int score, minScore;

    Player playerScript;
    public static bool overdrive= false;

    public void Start()
    {
        loadWeapon();
        weaponList[0].gameObject.SetActive(true);
        weaponList[1].gameObject.SetActive(false);
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Update()
    {
        if (Player.isDead)
        {
            death();
        }
        else
        {
            modeSelection();
            foreach (Weapons w in weaponList)
            {
                w.position();
                if (playerScript.cap >= 0.0f)
                    w.shoot();
            }
        }
    }

    public void death()    //death sequence of player
    {
        foreach (Weapons w in weaponList)
        {
            Rigidbody[] wrb = w.GetComponentsInChildren<Rigidbody>();
            Collider[] wcol = w.GetComponentsInChildren<Collider>();
            foreach (Rigidbody r in wrb)
                r.useGravity = true;
            foreach (Collider c in wcol)
                c.enabled = true;
        }
    }
   
    public void loadWeapon()  //preloads weapons into list to prevent null references
    {
        foreach (Weapons w in weaponBase)
        {
            weaponList.Add(Instantiate(w, transform.position, transform.rotation));
        }
    }

    public void modeSelection()  //toggles between loadout combinations
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (playerScript.cap > 0.0f && !overdrive)
            {
                weaponList[0].gameObject.SetActive(false);
                weaponList[2].gameObject.SetActive(false);
                weaponList[1].gameObject.SetActive(true);
                overdrive = true;
            }
            else
            {
                weaponList[1].gameObject.SetActive(false);
                weaponList[0].gameObject.SetActive(true);
                weaponList[2].gameObject.SetActive(true);
                overdrive = false;
            }
        }
    }
}

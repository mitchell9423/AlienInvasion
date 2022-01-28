using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    float timeRemaining;
    public static bool isDead = false, overdrive;
    public AudioSource[] clips;
    public AudioSource switcher;
    public LightningBoltScript[] lightning;
    public NavMeshAgent agent;
    public AudioClip[] soundClips;
    public Transform tr;
    public Animator deathscene;
    public Collider playerCol;
    public LootCrate nanite_box;

    //public AudioSource audioSource;

    Vector3 movement;
    public Rigidbody playerRigidbody;
    Save_Script save;
    int floorMask;
    float camRayLength = 100f, moveHorizontal, moveVertical;
    bool isAntigrave;

    float timer;

    //convert to private
    public float cost;
    public float playerHealth;
    public float maxHealth;
    public float recharge;
    public float cap;
    public float maxCap;
    public float fireRate;
    public float speed;


    public void Start()
    {
        save = Save_Script.Instance;
        floorMask = LayerMask.GetMask("Floor");
        playerHealth = maxHealth * save.healthMod;
        cap = maxCap * save.capMod;
    }

    void Update()  //FixedUpdate gathers input values and calls the Move and Turn functions
    {
        Charge_Capacitor();

        if (isDead)
            death();

        if (!isDead)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            move();
            Turning();
            AntiGrave();
        }

        if (playerHealth <= 0.0f && !isDead)
        {
            timeRemaining = 2.0f;
            foreach (LightningBoltScript light in lightning)
                light.EndPosition.y = 0.0f;

            foreach (AudioSource audio in clips)
                audio.enabled = false;

            isDead = true;
            switcher.clip = soundClips[0];
            switcher.Play();
            playerCol.isTrigger = false;
            playerRigidbody.drag = 20;
            playerRigidbody.freezeRotation = false;
            playerRigidbody.useGravity = true;
            agent.baseOffset = 0.0f;
            agent.enabled = false;
            deathscene.SetTrigger("dead");
        }
    }
    
    public void Charge_Capacitor()
    {
        float max = maxCap * save.capMod;
        float current = cap;
        float used = max - current;
        float newRecharge = (used / max) * recharge;

        if (timer <= 0.0f && !isDead)
        {
            if (cap + newRecharge >= max)
                cap = max;
            else
                cap += newRecharge;
            timer = 1f;
        }
        timer -= Time.deltaTime;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("eBullet"))
        {
            Save_Script.streak = 0;
            playerHealth--;
            switcher.PlayOneShot(soundClips[1]);
        }
        
        if (other.gameObject.CompareTag("loot"))
        {
            float charge;
            if (other.gameObject.name == "Shells(Clone)")
            {
                Weapon_Three.shells += 3;
                if (Weapon_Three.shells > 9)
                    Weapon_Three.shells = 9;
            }

            if (other.gameObject.name == "mBattery(Clone)")
            {
                charge = UnityEngine.Random.Range(10.0f, 50.0f);
                if (cap + charge >= maxCap * save.capMod)
                    cap = maxCap * save.capMod;
                else
                    cap += charge;

                charge = UnityEngine.Random.Range(10.0f, 50.0f);
                if (playerHealth + charge >= maxHealth * save.healthMod)
                    playerHealth = maxHealth * save.healthMod;
                else
                    playerHealth += charge;
            }

            if (other.gameObject.name == "sBattery(Clone)")
            {
                charge = UnityEngine.Random.Range(10.0f, 20.0f);
                if (playerHealth + charge >= maxHealth * save.healthMod)
                    playerHealth = maxHealth * save.healthMod;
                else
                    playerHealth += charge;
            }
            Destroy(other.gameObject);
        }

    }
    
    public void death()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining > 0)
        {
            tr.Rotate(0.0f, 0.0f, 0.2f);
        }
        else
            playerRigidbody.drag = 1;
    }

    private void AntiGrave()
    {
        if (agent.baseOffset > 1.6f)
            agent.baseOffset -= Time.deltaTime * 4;
        else
        {
            agent.baseOffset = 1.6f;
        }

        foreach (AudioSource audio in clips)
            audio.volume = agent.baseOffset / 50.0f;

        foreach (LightningBoltScript light in lightning)
        {
            light.EndPosition.y = (agent.baseOffset + 1.0f) * -1.0f;
        }
    }
    
    private void move()
    {
        if (!isDead)
        {
            movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            movement.Normalize();
            GetComponent<Rigidbody>().AddForce(movement * speed * save.speedMod, ForceMode.Impulse);
        }
    }
    
    public void Turning()  
    {
        if (!isDead)
        {
            RaycastHit floorHit;
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0.0f;
                Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);
                playerRigidbody.MoveRotation(newRotatation);
            }
        }
    }
}

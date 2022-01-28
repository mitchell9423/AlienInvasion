using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    private Transform playerLocation;
    private Vector3 offset;

    public float smooth = 5f;

    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        offset.Set(0.0f, 30f, -18f);
    }

    void FixedUpdate() //camera follows the player using smooth to buffer suddent direction changes
    {
        Vector3 targetCamPosition = playerLocation.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, smooth * Time.deltaTime);
    }
}

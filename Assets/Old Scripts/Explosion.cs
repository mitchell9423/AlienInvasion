using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject destroy;
    public float power = 10.0f, radius = 5.0f, upforce = 1.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (destroy == enabled)
            Invoke("Detonate", 4);
	}

    void Detonate()
    {
        Vector3 explosionPostion = destroy.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPostion, radius);
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(power, explosionPostion, radius, upforce, ForceMode.Impulse);
        }
    }
}

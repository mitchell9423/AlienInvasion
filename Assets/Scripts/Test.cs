
using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Test : MonoBehaviour
{
    public MeshRenderer render;
    public AudioSource sound;

    public float radius;
    public float power;
    public float upforce;
    Enemies escript;
    public Animator boom;

    bool detonated = false;

    public void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Mortor"))
        {
            Debug.Log("good");
            sound.Play();
            boom.SetTrigger("pow");
            if (!detonated)
            {
                Debug.Log("good");
                Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
                foreach (Collider hit in colliders)
                {
                    if (hit.gameObject.CompareTag("Enemy"))
                    {
                        Rigidbody rb = hit.GetComponent<Rigidbody>();
                        if (rb != null)
                            rb.AddExplosionForce(power, transform.position, radius, upforce, ForceMode.Impulse);
                        escript = hit.gameObject.GetComponent<Enemies>();
                        escript.health -= 1000;
                    }
                }
                render.enabled = false;
                Destroy(gameObject, 2.0f);
            }

            detonated = true;
        }
    }
}

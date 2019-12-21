using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius;

    public float force;

    void Start()
    {
        Collider [] ObjectsColliding = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider objectColliding in ObjectsColliding)
        {
            Rigidbody rb = objectColliding.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
        Destroy(gameObject);
    }

}

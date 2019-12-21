using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleDamage : MonoBehaviour
{
    public float damage;

    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other is CharacterController)
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            if (playerHealth != null)
                playerHealth.takeDamage(damage);
        }
    }
}

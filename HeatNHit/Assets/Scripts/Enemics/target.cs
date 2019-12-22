using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public Rigidbody rb;
    public Enemy EnemyScript;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        EnemyScript = transform.GetComponent<Enemy>();
    }
    public void takeDamage(float damage, float impactForce, Vector3 point, Vector3 normal)
    {

        if (EnemyScript != null)
        {
            EnemyScript.TakeDamage(damage);
            EnemyScript.AddKnockBack(impactForce);
        }
        else
        {
            Vector3 forca = -normal * impactForce;
            rb.AddForce(forca, ForceMode.Impulse);
        }
        
    }

}

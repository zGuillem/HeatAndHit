using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public Rigidbody rb;
    public Enemy EnemyScript;

    public void takeDamage(float damage, float impactForce, Vector3 point, Vector3 normal)
    {
        /*Vector3 forca = -normal * impactForce;
        rb.AddForce(forca, ForceMode.Impulse);*/
        if (EnemyScript != null)
        {
            EnemyScript.TakeDamage(damage);
            EnemyScript.AddKnockBack(impactForce);
        }
        

    }

}

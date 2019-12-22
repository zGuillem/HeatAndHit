using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControlerScript : MonoBehaviour
{
   
    void Update()
    {
        int i = 0;
        bool trobat = false;
        while ( i < transform.childCount && !trobat)
        {
            Transform ps = transform.GetChild(i);
            if (ps.GetComponent<ParticleSystem>().IsAlive())
            {
                trobat = true;
            }
            else
            {
                i++;
            }
        }
        if ( i == transform.childCount)
        {
            Destroy(gameObject);
        }
    }
}

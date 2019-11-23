using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecoil : MonoBehaviour
{
    // Start is called before the first frame update

    public void recoil(float s)
    {
        transform.Rotate(Vector3.left * s);
    }

}
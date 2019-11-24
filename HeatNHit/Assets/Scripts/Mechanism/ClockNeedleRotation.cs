using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockNeedleRotation : MonoBehaviour
{

    public Transform secNeedle;
    public Transform minNeedle;

    private float waitTime;
    private Vector3 rotationVector = new Vector3(0f, 360 / 60, 0f);

    void Start()
    {
       waitTime = Time.time + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime <= Time.time)
        {
            rotateNeedle(secNeedle);
            waitTime = Time.time + 0.5f;
        }
    }

    void rotateNeedle(Transform Needle)
    {
        Needle.Rotate(rotationVector);
    }
}

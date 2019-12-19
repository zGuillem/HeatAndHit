using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockNeedleRotation : MonoBehaviour
{

    public Transform secNeedle;
    public Transform minNeedle;

    public float secSpeed = 60f;
    public float minSpeed = 5f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rotateNeedle(secNeedle, new Vector3(0f, secSpeed * Time.deltaTime, 0f));
        rotateNeedle(minNeedle, new Vector3(0f, minSpeed * Time.deltaTime, 0f));
    }

    void rotateNeedle(Transform Needle, Vector3 value)
    {
        Needle.Rotate(value);
    }
}

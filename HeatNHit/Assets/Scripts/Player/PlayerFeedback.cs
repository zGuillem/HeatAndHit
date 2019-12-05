using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    public Camera cam;

    public float screenShakeForce = 0.2f;
    public float screenShakeTime = 0.2f;

    public GameObject[] weaponsList;

    public int numOfBlinks = 5;

    private CameraShaker shaker;
    private List<gunScript> gunScriptList = new List<gunScript>();

    void Start()
    {
        shaker = cam.GetComponent<CameraShaker>();
        Debug.Assert(shaker != null);

        for (int i = 0; i < weaponsList.Length; i++){ 
            gunScript gun = weaponsList[i].GetComponent<gunScript>();

            gunScriptList.Add(gun);
        }
    }

    public void gotHit(float inmuneTime)
    {
        shaker.Shake(screenShakeTime, screenShakeForce);
        StartCoroutine(blink(Time.time + inmuneTime, inmuneTime / numOfBlinks));
    }

    private IEnumerator blink(float endTime, float changePhaseTime)
    {
        float nextChange = Time.time + changePhaseTime;
        changeWeaponVisualization(false);
        bool actual = true;

        while(endTime > Time.time)
        {
            if(nextChange < Time.time)
            {
                changeWeaponVisualization(actual);
                actual = !actual;
                nextChange = Time.time + changePhaseTime;
            }

            yield return null;
        }

        changeWeaponVisualization(true);
        

    }

    private void changeWeaponVisualization(bool value)
    {
        foreach(gunScript g in gunScriptList)
        {
            if (g.activada)
            {
                g.setArmaMesh(value);
                break;
            }
        }
    }
}

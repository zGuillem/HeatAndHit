﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFeedback : MonoBehaviour
{
    public Camera cam;
    public GameObject weaponSwitcher;

    public float screenShakeForce = 0.2f;
    public float screenShakeTime = 0.2f;

    public GameObject[] weaponsList;

    public int numOfBlinks = 5;

    public PlayerScore scoreController;

    public GameObject DeathHud;
    public GameObject mirilla;

    private CameraShaker shaker;
    private DeathEffectShader deathEffect;
    private List<gunScript> gunScriptList = new List<gunScript>();

    private playerAudio audioController;

    private float loopMovement = 0f;

    void Start()
    {
        shaker = cam.GetComponent<CameraShaker>();
        Debug.Assert(shaker != null);

        deathEffect = cam.GetComponent<DeathEffectShader>();
        Debug.Assert(deathEffect != null);

        for (int i = 0; i < weaponsList.Length; i++){ 
            gunScript gun = weaponsList[i].GetComponent<gunScript>();

            gunScriptList.Add(gun);
        }

        audioController = GetComponentInChildren<playerAudio>();
        Debug.Assert(audioController != null);

    }

    public void gotHit(float inmuneTime)
    {
        audioController.playHurt();
        shaker.Shake(screenShakeTime, screenShakeForce);
        StartCoroutine(blink(Time.time + inmuneTime, inmuneTime / numOfBlinks));
    }

    public void killed()
    {
        mirilla.SetActive(false);
        audioController.playDying();
        deathEffect.killStart();
        StartCoroutine(timeSlow());
        scoreController.animationToCenter();
        DeathHud.GetComponent<deathHudController>().Show();
        cam.GetComponent<CamLook>().DesfixarCursor();
    }

    public IEnumerator timeSlow()
    {
        while (Time.timeScale > 0f)
        {
            float aux = Time.timeScale - (Time.unscaledDeltaTime/2);
            if (aux > 0f)
                Time.timeScale = aux;
            else
                Time.timeScale = 0;
            yield return null;
        }
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

    public void playerMoving(bool value)
    {
        if (value)
        {
            loopMovement += Time.deltaTime;

            if (loopMovement > Mathf.PI * 2)
                loopMovement = 0f;

            weaponSwitcher.transform.localPosition = new Vector3(0.02f * Mathf.Cos(loopMovement * 10), 0.01f * Mathf.Cos(loopMovement * 20), 0f);
            cam.transform.localPosition = new Vector3(0f + 0.05f * Mathf.Cos(loopMovement * 10), 0.9f + 0.02f * Mathf.Cos(loopMovement * 20), 0f + 0f);
        }

    }
}

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

    public PlayerScore scoreController;

    public GameObject DeathHud;

    private CameraShaker shaker;
    private DeathEffectShader deathEffect;
    private List<gunScript> gunScriptList = new List<gunScript>();

    private playerAudio audioController;

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
        Time.timeScale = 1;
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

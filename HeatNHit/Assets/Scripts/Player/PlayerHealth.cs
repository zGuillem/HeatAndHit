using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class PlayerHealth : MonoBehaviour
{

    public GameObject vidaObject;
    private lifeCounter vidaScript;
    public float maxHealth = 5;
    public PlayerFeedback feedback;
    public PlayerDisable playerDisable;

    public float inmuneTime = 0.5f;

    private float nextInmuneTime = 0;

    private float currentHealth;

    public bool killed = false;

    
    void Start()
    {
        currentHealth = maxHealth;
        vidaScript = vidaObject.GetComponent<lifeCounter>();
        vidaScript.maxLife((int)maxHealth, (int)currentHealth);
        updateScreen();
    }

    //Update health

    void Update()
    {
        if (Input.GetButtonDown("AutoDamage"))
        {
            updateHealth(-1);
        }
    }

    public void takeDamage(float value)
    {
        if (Time.time > nextInmuneTime && !killed)
        {
            nextInmuneTime = Time.time + inmuneTime;
            updateHealth(-value);
            feedback.gotHit(inmuneTime);
        }
    }

    public void killPlayer()
    {
        updateHealth(-currentHealth);
    }

    public void updateHealth(float value)
    {
        currentHealth += value;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);

        if (!killed && currentHealth == 0.0f)
        {
            killed = true;
            playerDisable.disableAll();
            feedback.killed();
        }
        updateScreen();
    }

    private void updateScreen()
    {
        vidaScript.actualLife((int)currentHealth);
    }

    private float mapLifeToValue()
    {
        return currentHealth / maxHealth;
    }


}

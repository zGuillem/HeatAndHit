using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class PlayerHealth : MonoBehaviour
{

    public GameObject aux;
    private Image obtingut;
    public float maxHealth = 100;

    private float currentHealth = 100;
    void Start()
    {
        obtingut = aux.GetComponent<Image>();
        updateScreen();
    }

    //Update health

    void Update()
    {
        if (Input.GetButtonDown("AutoDamage"))
        {
            updateHealth(10);
        }

    }

    public void updateHealth(float value)
    {
        currentHealth -= value;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);

        if (currentHealth == 0.0f)
        {
            //Debug.Log("MORT"); ---------------------------------------------------------
        }
        updateScreen();
    }

    private void updateScreen()
    {
        obtingut.fillAmount = mapLifeToValue();
    }

    private float mapLifeToValue()
    {
        return currentHealth / maxHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public Slider pixelEffectSlider;
    public Slider fovSlider;

    private bool inPauseMenu = false;
    private float preTimeScale = 1;
    private PlayerHealth accesKilled;
    // Start is called before the first frame update
    void Start()
    {
        accesKilled = player.GetComponent<PlayerHealth>();

        float aux = PlayerPrefs.GetFloat("PixelEffect", 150f);
        if (aux == -1)
            pixelEffectSlider.value = pixelEffectSlider.maxValue;
        else 
            pixelEffectSlider.value = aux;

        fovSlider.value = PlayerPrefs.GetFloat("fov", 60f);

        hide();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            inPauseMenu = !inPauseMenu;
            if (inPauseMenu)
                hide();
            else
                show();

        }
    }

    private void show()
    {
        preTimeScale = Time.timeScale;
        Time.timeScale = 0;
        AudioListener aux = cam.GetComponent<AudioListener>();
        Debug.Assert(aux != null);

        AudioListener.pause = true;


        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }

        CamLook comprovant = cam.GetComponent<CamLook>();
        Debug.Assert(comprovant != null);
        comprovant.DesfixarCursor();
    }

    private void hide()
    {
        Time.timeScale = preTimeScale;
        AudioListener.pause = false;

        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        CamLook comprovant = cam.GetComponent<CamLook>();
        Debug.Assert(comprovant != null);
        if(!accesKilled.killed)
            comprovant.FixarCursor();
    }

    public void onChangeSlider()
    {
        PixelateEffect comprovant = cam.GetComponent<PixelateEffect>();
        Debug.Assert(comprovant != null);

        float newValue = pixelEffectSlider.value;

        if (newValue == pixelEffectSlider.maxValue)
        {
            newValue = -1;
            comprovant.enabled = false;
        }
        else
        {
            comprovant.enabled = true;
            comprovant.verticalPixels = newValue;
        }

        PlayerPrefs.SetFloat("PixelEffect", newValue);
        PlayerPrefs.Save();
    }

    public void onChangeSliderFOV()
    {
        PlayerPrefs.SetFloat("fov", fovSlider.value);
        cam.fieldOfView = fovSlider.value;

        PlayerPrefs.Save();
    }

    public void onResumeButton()
    {
        inPauseMenu = false;
        hide();
    }
}

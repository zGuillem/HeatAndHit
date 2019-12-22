using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public Camera cam;
    public Slider pixelEffectSlider;

    private bool inPauseMenu = false;
    private float preTimeScale = 1;
    // Start is called before the first frame update
    void Start()
    {
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
        aux.enabled = false;


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
        AudioListener aux = cam.GetComponent<AudioListener>();
        Debug.Assert(aux != null);
        aux.enabled = true;

        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        CamLook comprovant = cam.GetComponent<CamLook>();
        Debug.Assert(comprovant != null);
        comprovant.FixarCursor();
    }

    public void onChangeSlider()
    {
        PixelateEffect comprovant = cam.GetComponent<PixelateEffect>();
        Debug.Assert(comprovant != null);

        float newValue = pixelEffectSlider.value;

        if (newValue == pixelEffectSlider.maxValue)
            comprovant.enabled = false;
        else
        {
            comprovant.enabled = true;
            comprovant.verticalPixels = newValue;
        }
    }

    public void onResumeButton()
    {
        inPauseMenu = false;
        hide();
    }
}

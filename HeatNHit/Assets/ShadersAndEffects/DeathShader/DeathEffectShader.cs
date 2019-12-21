using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //See changes on editor
public class DeathEffectShader : MonoBehaviour
{
    public Camera usedCamera;
    public Material effectMaterial;

    private float tValue = 1.0f;
    //MIRAR SI ES POT FER UN ACTIVATE

    private void Update()
    {
        effectMaterial.SetFloat("_tValue", tValue);
    }

    public void killStart()
    {
        StartCoroutine(decreaseTValue());
    }

    private IEnumerator decreaseTValue()
    {
        while(tValue > 0f)
        {
            tValue -= Time.unscaledDeltaTime/2;
            if (tValue < 0f)
                tValue = 0f;

            yield return null;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {   //Apply the shader
        Graphics.Blit(source, destination, effectMaterial);
    }
}

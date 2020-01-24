using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to apply the pixelate effect  
//Author: Oriol Perarnau Arnau

[ExecuteInEditMode] //See changes on editor
public class PixelateEffect : MonoBehaviour
{
    public Camera usedCamera;                   //Camera to pixelate
    public float verticalPixels = 10f;          //Number of pixels in the Y axis, we do it like this to make always the same number of "pixels" evading problems with the resolution
    public Material effectMaterial;             //Shader Material to apply
    
    private void Start()
    {   //Called on the start
        gameData data = saveSystem.LoadData();
        if (data != null && data.PixelEffect != -1)
            verticalPixels = data.PixelEffect;
        shaderUpdate();
    }

    private void Update()
    {
        shaderUpdate();
    }
    
    private void shaderUpdate()
    {   //Update values of the shaders
        float mida = usedCamera.pixelHeight / verticalPixels;
        float horitzontalPixels = usedCamera.pixelWidth / mida;

        effectMaterial.SetFloat("_Columns", horitzontalPixels);
        effectMaterial.SetFloat("_Rows", verticalPixels);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {   //Apply the shader
        Graphics.Blit(source, destination, effectMaterial);
    }
}


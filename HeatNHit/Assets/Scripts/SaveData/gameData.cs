using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class gameData
{
    public float PixelEffect;   //Quantitat de "pixelacio" de la pantalla
    public int Highscore;     //maxima puntuacio del jugador

    public gameData(float pixelEffect, int highscore)
    {
        PixelEffect = pixelEffect;
        Highscore = highscore;
    }

    public gameData(float pixelEffect)
    {
        PixelEffect = pixelEffect;
        Highscore = -1;
    }

    public gameData(int highscore)
    {
        Highscore = highscore;
        PixelEffect = -1;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine;

public class PlayerScore : MonoBehaviour
{

    public GameObject scoreText;

    private int score;
    private int highScore;
    private Text scoreTextText;


    void Start()
    {
        score = 0;
        highScore = PlayerPrefs.GetInt("highscore", 0);

        scoreTextText = scoreText.GetComponent<Text>();
        Debug.Assert(scoreTextText != null);
    }

    public void ScorePlus(int value)
    {
        score += value;
        UpdateText();
    }

    public void animationToCenter()
    {
        StartCoroutine(movingToCenter());

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
            PlayerPrefs.Save();
        }
    }

    public IEnumerator movingToCenter()
    {//PARAMETRITZAR ARA ES MOLT CUTRE--------------------------------------------------------------------------
        RectTransform rectText = scoreText.GetComponent<RectTransform>();
        Debug.Assert(rectText != null);
        
        while((rectText.localPosition).magnitude > 4)
        {
            rectText.localPosition = rectText.localPosition * 0.85f;
            rectText.localScale += new Vector3(1f, 1f, 1f) * Time.unscaledDeltaTime;
            yield return null;
        }

        rectText.localPosition = new Vector3(0f, 0f, 0f);
    }

    void UpdateText()
    {
        scoreTextText.text = score.ToString();
    }
}

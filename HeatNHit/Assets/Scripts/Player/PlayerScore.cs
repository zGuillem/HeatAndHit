using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine;

public class PlayerScore : MonoBehaviour
{

    public GameObject scoreText;

    private int score;
    private Text scoreTextText;


    void Start()
    {
        score = 0;
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
    }

    public IEnumerator movingToCenter()
    {//PARAMETRITZAR ARA ES MOLT CUTRE--------------------------------------------------------------------------
        RectTransform rectText = scoreText.GetComponent<RectTransform>();
        Debug.Assert(rectText != null);
        
        while((rectText.localPosition).magnitude > 4)
        {
            rectText.localPosition = rectText.localPosition * 0.85f;// += (-rectText.localPosition).normalized * 100 * Time.unscaledDeltaTime;
            rectText.localScale += new Vector3(0.5f, 0.5f, 0.5f) * Time.unscaledDeltaTime;
            yield return null;
        }

        rectText.localPosition = new Vector3(0f, 0f, 0f);
    }

    void UpdateText()
    {
        scoreTextText.text = score.ToString();
    }
}

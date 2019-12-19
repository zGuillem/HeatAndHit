using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required when Using UI elements.
using UnityEngine;

public class PlayerScore : MonoBehaviour
{

    public GameObject scoreText;

    private int score;
    private Text scoreTextAsset;


    void Start()
    {
        score = 0;
        scoreTextAsset = scoreText.GetComponent<Text>();
        Debug.Assert(scoreTextAsset != null);

        scoreTextAsset.text = "HELLO";
    }

   
    void Update()
    {
        
    }
}

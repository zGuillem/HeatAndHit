using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class deathHudController : MonoBehaviour
{
    // Start is called before the first frame update
    public Text highscoreText;

    void Start()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    
    public void Show()
    {
        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }

        gameData g = saveSystem.LoadData();
        if (g != null && g.Highscore != -1)
            highscoreText.text = "HighScore: " + g.Highscore;
        else
            highscoreText.text = "HighScore: 0";        
    }

    public void playAgainButtonOnClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void menuButtonOnClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}

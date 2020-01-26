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

        highscoreText.text = "HighScore: " + PlayerPrefs.GetInt("highscore", 0); 
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

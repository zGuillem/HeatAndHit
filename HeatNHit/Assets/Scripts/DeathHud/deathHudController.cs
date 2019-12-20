using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathHudController : MonoBehaviour
{
    // Start is called before the first frame update
    

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
    }

    public void playAgainButtonOnClicked()
    {
        Debug.Log("Hrllo");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void menuButtonOnClicked()
    {
        Debug.Log("hola");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}

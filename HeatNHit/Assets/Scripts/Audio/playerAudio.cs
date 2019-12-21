using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Oriol Perarnau Arnau
public class playerAudio : MonoBehaviour
{

    //Class to manage better all audio clips of player

    public AudioSource hurtSound;                      //Sound of shooting

    public void playHurt()
    {
        //Plays shoot sound

        hurtSound.Play();
    }
}


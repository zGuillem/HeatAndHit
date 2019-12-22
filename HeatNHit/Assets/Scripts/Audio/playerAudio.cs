using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Oriol Perarnau Arnau
public class playerAudio : MonoBehaviour
{

    //Class to manage better all audio clips of player

    public AudioSource hurtSound;                      //Sound of player getting hurt
    public AudioSource deathSound;                      //Sound of player dying

    public void playHurt()
    {
        //Plays hurt sound

        hurtSound.Play();
    }

    public void playDying()
    {
        //Plays dying sound

        deathSound.Play();
    }
}


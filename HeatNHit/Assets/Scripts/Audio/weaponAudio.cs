using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Oriol Perarnau Arnau
public class weaponAudio : MonoBehaviour
{

    //Class to manage better all audio clips of a weapon

    public AudioSource shootSound;                      //Sound of shooting
    public AudioSource burnOut;                         //Sound of broking
    public AudioSource coldOut;                         //Sound of fixing after time

    public void playShoot()
    {
        //Plays shoot sound

        shootSound.Play();
    }

    public void playBurnOut()
    {
        //Plays the broking sound

        burnOut.Play();
    }

    public void plaplayColdOut()
    {   
        //Plays the fixing soundç

        coldOut.Play();
    }
}


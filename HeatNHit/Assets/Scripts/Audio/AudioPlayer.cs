using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource intro;
    public AudioSource loop;

    public void changeVolume(float value)
    {
        intro.volume = value;
        loop.volume = value;
    }

    private void Update()
    {
        if (!intro.isPlaying && !loop.isPlaying)
        {
            loop.Play();
        }
    }
}

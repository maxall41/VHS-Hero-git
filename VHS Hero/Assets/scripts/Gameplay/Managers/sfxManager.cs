using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    public AudioSource timeTravelSFX;
    public AudioSource pullbackSFX;
    public AudioSource jump;
    public AudioSource walk;
    public AudioSource keyGrab;


    public void F_timeTravel()
    {
        timeTravelSFX.Play();
    }

    public void F_pullback()
    {
        pullbackSFX.Play();
    }

    public void F_keyGrab()
    {
        keyGrab.Play();
    }

    public void F_jump ()
    {
        jump.Play();
    }

    public void F_walk ()
    {
        walk.Play();
    }

    
}

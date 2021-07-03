using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPlayer : MonoBehaviour
{
    public Tutorial control;
    public bool playOnNewLevel = true;



    public void NextLevel()
    {
        if (playOnNewLevel == true)
        {
            control.NextSentence();
        }
    }
}

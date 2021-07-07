using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameplayParams : MonoBehaviour
{

    public List<string> load = new List<string>();

    public List<string> empty = new List<string>();

    void Start()
    {
        load = ES3.Load("gameplayParams", empty);

        foreach(string loaded in load)
        {
            PlayerPrefs.SetInt(loaded, 1);
        }
        PlayerPrefs.SetInt("KeyHintShown", 1);
        PlayerPrefs.SetInt("TimeTravelHint", 1);
    }

}

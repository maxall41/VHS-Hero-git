using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ResetGameplayParams : MonoBehaviour
{

    public List<string> load = new List<string>();

    public List<string> empty = new List<string>();

    void Start()
    {
        load = ES3.Load("gameplayParams", empty);


        // Removed all duplicates
        load = load.Distinct().ToList();

        ES3.Save("gameplayParams", load); 

        foreach (string loaded in load)
        {
            PlayerPrefs.SetInt(loaded, 1);
        }
        PlayerPrefs.SetInt("KeyHintShown", 1);
        PlayerPrefs.SetInt("TimeTravelHint", 1);
    }

}

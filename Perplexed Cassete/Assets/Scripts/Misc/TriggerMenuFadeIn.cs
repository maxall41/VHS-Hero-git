using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMenuFadeIn : MonoBehaviour
{

    void Start()
    {
        PlayerPrefs.SetInt("fadeInMenu", 1);
    }
}

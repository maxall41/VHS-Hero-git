using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Albedo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerPrefs.SetInt("Albedo", 1);
    }
}

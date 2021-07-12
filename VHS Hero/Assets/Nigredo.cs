using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nigredo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerPrefs.SetInt("Nigredo", 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citrinitas : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerPrefs.SetInt("Citrinitas", 1);
        Destroy(gameObject);
    }
}

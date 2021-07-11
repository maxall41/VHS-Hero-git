using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOpenedR : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.GetInt("Rubedo") == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}

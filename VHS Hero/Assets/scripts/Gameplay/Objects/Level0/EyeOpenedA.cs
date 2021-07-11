using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOpenedA : MonoBehaviour
{
    void Start()
    {
        if (GameObject.Find("Player").GetComponent<PlayerDataHolder>().Albedo == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}

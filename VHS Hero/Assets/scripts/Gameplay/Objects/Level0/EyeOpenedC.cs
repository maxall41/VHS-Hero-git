using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOpenedC : MonoBehaviour
{
    void Start()
    {
        if (GameObject.Find("Player").GetComponent<PlayerDataHolder>().Citrinitas == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}

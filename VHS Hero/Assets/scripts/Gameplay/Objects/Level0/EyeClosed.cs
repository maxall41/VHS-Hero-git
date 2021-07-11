using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeClosed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Rubedo") == 1 
            || GameObject.Find("Player").GetComponent<PlayerDataHolder>().Albedo == true 
            || GameObject.Find("Player").GetComponent<PlayerDataHolder>().Nigredo == true
            || GameObject.Find("Player").GetComponent<PlayerDataHolder>().Citrinitas==true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }


}

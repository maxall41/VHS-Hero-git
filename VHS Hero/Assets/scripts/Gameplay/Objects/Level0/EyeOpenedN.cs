using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeOpenedN : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Player").GetComponent<PlayerDataHolder>().Nigredo == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nigredo : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDataHolder>().Nigredo = true;
            GameObject.Find("EyeOpenedR").GetComponent<SpriteRenderer>().enabled = true;
            Destroy(gameObject);

        }
    }
}

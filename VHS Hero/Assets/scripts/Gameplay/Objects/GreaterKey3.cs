using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterKey3 : MonoBehaviour
{
    // Start is called before the first frame update

    private bool watchForPickup;
    EffectOnPick effect = new EffectOnPick();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            watchForPickup = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            watchForPickup = false;
        }
    }


    private void Update()
    {
        if (watchForPickup == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("Player").GetComponent<PlayerDataHolder>().ThirdGreaterKey = true;
                effect.PickGreaterKey();
                Destroy(gameObject);
            }
        }
    }
}

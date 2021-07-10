using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterKey3 : MonoBehaviour
{
    // Start is called before the first frame update

    private bool watchForPickup;
    EffectOnPick effect = new EffectOnPick();

    public GameObject greaterKeyUI;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDataHolder>().ThirdGreaterKey = true;
            effect.PickGreaterKey();
            greaterKeyUI.SetActive(true);
            Destroy(gameObject);

        }
    }

}

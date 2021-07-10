using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterKey2 : MonoBehaviour
{
    private bool watchForPickup;
    EffectOnPick effect = new EffectOnPick();

    public GameObject greaterKeyUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDataHolder>().SecondGreaterKey = true;
            effect.PickGreaterKey();
            greaterKeyUI.SetActive(true);
            Destroy(gameObject);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterKey2 : MonoBehaviour
{
    private bool watchForPickup;

    public GameObject greaterKeyUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDataHolder>().hasSecondGreaterKey = true;
            greaterKeyUI.SetActive(true);
            Destroy(gameObject);

        }
    }

}

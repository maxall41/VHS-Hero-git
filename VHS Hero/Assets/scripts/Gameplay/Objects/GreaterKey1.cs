using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterKey1 : MonoBehaviour
{
    private bool watchForPickup;

    public GameObject greaterKeyUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDataHolder>().FirstGreaterKey = true;
            greaterKeyUI.SetActive(true);
            Destroy(gameObject);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    private bool watchForPickup;

    public List<string> lrh = new List<string>();

    public GameObject Knob;

    private void Start()
    {
        Knob = GameObject.Find("RefHolder").GetComponent<RefHolder>().knob;
    }

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
            if (Input.GetKeyDown(KeyCode.E) && GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey == false)
            {
                GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey = true;
                lrh = GameObject.Find("levelman").GetComponent<LevelManager>().keysPicked;
                lrh.Add(gameObject.name);
                Knob.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}

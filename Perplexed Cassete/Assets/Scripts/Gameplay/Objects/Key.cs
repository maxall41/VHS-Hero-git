using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lean.Pool;

public class Key : MonoBehaviour
{

    private bool watchForPickup;

    private List<string> lrh = new List<string>();

    public GameObject Knob;

    private LevelManager levelman;

    private PlayerDataHolder dataHolder;

    private void Start()
    {
        Knob = GameObject.Find("RefHolder").GetComponent<RefHolder>().keyUI;
        levelman = GameObject.Find("levelman").GetComponent<LevelManager>();
        dataHolder = GameObject.Find("Player").GetComponent<PlayerDataHolder>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered " + collision.gameObject.name);
        Debug.Log("KEY: " + dataHolder.holdingKey);
        if (dataHolder.holdingKey == false)
        {
            dataHolder.holdingKey = true;
            lrh = levelman.keysPicked;
            lrh.Add(gameObject.name);
            Knob.SetActive(true);
            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_keyGrab();

            // Disable
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.enabled = false;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        watchForPickup = false;
    //    }
    //}


    //private void Update()
    //{
    //    if (watchForPickup == true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.E) && GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey == false)
    //        {
    //            GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey = true;
    //            lrh = GameObject.Find("levelman").GetComponent<LevelManager>().keysPicked;
    //            lrh.Add(gameObject.name);
    //            Knob.SetActive(true);
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}

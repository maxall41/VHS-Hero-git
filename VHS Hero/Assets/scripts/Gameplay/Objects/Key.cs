using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{

    private bool watchForPickup;

    public List<string> lrh = new List<string>();

    public TextMeshProUGUI hintText;

    public string hint;

    public GameObject Knob;

    private void Start()
    {
        Knob = GameObject.Find("RefHolder").GetComponent<RefHolder>().knob;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hintText.text = hint;
       if (PlayerPrefs.GetInt("KeyHintShown") != 0)
        {
            StartCoroutine(Type(hintText, hint, 0.02F));
            PlayerPrefs.SetInt("KeyHintShown", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey == false)
        {
            GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey = true;
            lrh = GameObject.Find("levelman").GetComponent<LevelManager>().keysPicked;
            lrh.Add(gameObject.name);
            Knob.SetActive(true);
            Destroy(gameObject);
        }
    }

    IEnumerator Type(TextMeshProUGUI text, string textToType,float typingSpeed)
    {
        foreach (char letter in textToType.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
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

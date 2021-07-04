using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{

    public GameObject levelManager;

    private bool watchForE = false;

    public GameObject Knob;

    private void Start()
    {
        Knob = GameObject.Find("RefHolder").GetComponent<RefHolder>().knob;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            watchForE = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            watchForE = false;
        }
    }

    private void Update()
    {
        if (watchForE == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey == true)
            {
                Knob.SetActive(false);
                GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey = false;
                GameObject.Find("levelman").GetComponent<LevelManager>().NextLevel();
            }
        }
    }
}

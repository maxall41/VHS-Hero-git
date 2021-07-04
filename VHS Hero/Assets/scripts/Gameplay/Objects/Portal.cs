using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{

    public GameObject levelManager;

    private bool watchForE = false;

    public GameObject Knob;

    public int levelDetector;

    private void Start()
    {
        levelDetector = GameObject.Find("levelman").GetComponent<LevelManager>().CurrentLevel;
        //Only active current level's portal
        if (this.gameObject.tag != (levelDetector.ToString()))
        {
            this.gameObject.SetActive(false);
        }
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

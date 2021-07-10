using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{

    public GameObject levelManager;

    private bool watchForE = false;

    public GameObject Knob;

    public string levelID;

    private bool active = true;
   

    private void Start()
    {
        
        Knob = GameObject.Find("RefHolder").GetComponent<RefHolder>().knob;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if portal is active and player has key before activating. We will make some closed door art in the the future for the deactivated state
        if (GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey == true && active == true)
        {
            Knob.SetActive(false);
            GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey = false;
            GameObject.Find("levelman").GetComponent<LevelManager>().NextLevel();
        }
    }

    public void TimelineMovementEvent()
    {
        // Deactivate portals in past/future levels
        int levelDetector = GameObject.Find("levelman").GetComponent<LevelManager>().CurrentLevel;
        Debug.Log("test -AB: ");
        Debug.Log(levelID);
        Debug.Log(levelDetector.ToString());
        if (levelID != levelDetector.ToString())
        {
            // Removed because it was causing issues with pooling
            //this.gameObject.SetActive(false);
            this.gameObject.name = "INACTIVE PORTAL";
            active = false;
        }
        else
        {
            this.gameObject.name = "ACTIVE PORTAL";
            active = true;
        }
    }


}

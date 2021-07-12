using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{

    public GameObject levelManager;

    public GameObject keyUI;

    public string levelID;

    private bool active = true;

    private LevelManager levelman;

    private PlayerDataHolder dataHolder;
   

    private void Start()
    {
        
        keyUI = GameObject.Find("RefHolder").GetComponent<RefHolder>().knob;
        levelman = GameObject.Find("levelman").GetComponent<LevelManager>();
        dataHolder = GameObject.Find("Player").GetComponent<PlayerDataHolder>();

    }

    void OnBecameVisible()
    {

        levelman.portalIndicatorEnabled = false;
    }

    void OnBecameInvisible()
    {
        levelman.portalIndicatorEnabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey == true && active == true)
        {
            keyUI.SetActive(false);
            dataHolder.holdingKey = false;
            levelman.NextLevel();
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
            this.gameObject.name = "INACTIVE PORTAL";
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            active = false;
        }
        else
        {
            this.gameObject.name = "ACTIVE PORTAL";
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            active = true;
        }
    }


}

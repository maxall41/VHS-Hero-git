using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class FinalDoor : MonoBehaviour
{

    public GameObject levelManager;

    private bool watchForE = false;

    public GameObject Knob;

    public string levelID;

    private bool active = true;

    private PlayerDataHolder playerDataHolder;


    private void Start()
    {

        Knob = GameObject.Find("RefHolder").GetComponent<RefHolder>().knob;
        playerDataHolder = GameObject.Find("Player").GetComponent<PlayerDataHolder>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        watchForE = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        watchForE = false;
    }


    private void Update()
    {
        if (watchForE == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (active == true)
                {
                    if (playerDataHolder.FirstGreaterKey == true)
                    {
                        if (playerDataHolder.SecondGreaterKey == true)
                        {
                            if (playerDataHolder.ThirdGreaterKey == true)
                            {
                                SceneManager.LoadScene("completeAllKeys");
                                PlayerPrefs.SetInt("Rubedo", 1);
                            }
                            else
                            {
                                SceneManager.LoadScene("completeNoKeys");
                                PlayerPrefs.SetInt("Delusion", 1);
                            }
                        }
                        else
                        {
                            SceneManager.LoadScene("completeNoKeys");
                            PlayerPrefs.SetInt("Delusion", 1);
                        }
                    }
                    else
                    {
                        SceneManager.LoadScene("completeNoKeys");
                        PlayerPrefs.SetInt("Delusion", 1);
                    }
                }
            }
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

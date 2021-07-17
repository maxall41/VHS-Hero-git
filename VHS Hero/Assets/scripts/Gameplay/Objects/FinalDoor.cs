using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class FinalDoor : MonoBehaviour
{

    public GameObject levelManager;

    private bool watchForE = false;

    public GameObject Knob;

    public string levelID;

    private bool active = true;

    private PlayerDataHolder playerDataHolder;

    public InputAction continueInput;

    private void OnEnable()
    {
        continueInput.Enable();
    }

    private void OnDisable()
    {
        continueInput.Disable();
    }




    private void Start()
    {

        Knob = GameObject.Find("RefHolder").GetComponent<RefHolder>().keyUI;
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
        //TODO: Replace with less horible logic tree:
        if (watchForE == true)
        {
            if (continueInput.triggered)
            {
                if (active == true)
                {
                    if (playerDataHolder.hasFirstGreaterKey == true)
                    {
                        if (playerDataHolder.hasSecondGreaterKey == true)
                        {
                            if (playerDataHolder.hasThirdGreaterKey == true)
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

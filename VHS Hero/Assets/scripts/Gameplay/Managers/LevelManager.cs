using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Lean.Pool;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    private int currentLevel = 0;
    public int CurrentLevel { get => currentLevel; } // used to inactivate portals in past / future levels.
    private float pullbackTimer;

    private GameObject lastLevel;

    public GameObject Knob;

    private bool pullbacked;

    public List<string> keysPicked = new List<string>();

    public List<string> buttonsActivated = new List<string>();

    private int lastLevelIndex;

    private float timeCooldown;

    private float cooldownDisplay = 3.5F;

    public TextMeshProUGUI travelStatusText;

    public GameObject flicker;

    public GameObject transHolder;

    public bool SnapOn = true;

    GameObject closetsObject;
    private float oldDistance = 9999;

    GameObject[] NearGameobjects;

    private float nextLevelCooldown; // Fixes issues with timing

    public Slider slider;

    private Vector3 playerStartPos;

    public GameObject Past;

    public GameObject Present;

    public GameObject Future;

    private Vector3 lastDoorPos;

    public TextMeshProUGUI hintText;

    public List<Vector3> instPos = new List<Vector3>();

    public string hint;

    private void Start()
    {
        lastLevel = LeanPool.Spawn(levels[currentLevel], instPos[currentLevel], Quaternion.identity);
        currentLevel++;
        playerStartPos = GameObject.Find("Player").transform.position;

        foreach (GameObject level in levels)
        {
            if (level == null)
            {
                Debug.LogError("Level in level manager is missing!");
            }
        }
    }


    private void InFuture()
    {
        Present.SetActive(false);
        Past.SetActive(false);
        Future.SetActive(true);
    }

    private void InPast()
    {
        Present.SetActive(false);
        Future.SetActive(false);
        Past.SetActive(true);
    }

    private void InPresent()
    {
        Future.SetActive(false);
        Past.SetActive(false);
        Present.SetActive(true);
    }

    public void Restart()
    {
        keysPicked.Clear();
        buttonsActivated.Clear();
        StartCoroutine("flicker");
        // Not using LeanPool because it causes issues with respawning objects
        Destroy(lastLevel);
        lastLevel = Instantiate(levels[currentLevel - 1], instPos[currentLevel - 1], Quaternion.identity);


        GameObject.Find("Player").transform.position = lastDoorPos;
           

        // Reset saved parameters
        GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey = false;

        Knob.SetActive(false);
    }

    private void Snap(GameObject gb)
    {
        if (SnapOn == true)
        {
            Debug.Log(gb.transform.position);
            // Snap to nearest point to prevent player falling off map.
            closetsObject = FindClosestSnap();
            gb.transform.position = new Vector3(closetsObject.transform.position.x, closetsObject.transform.position.y, 0);
            //gb.transform.position = new Vector3(gb.transform.position.x, gb.transform.position.y, 0);
            Debug.Log("Snapped to " + closetsObject.name);
        }
    }

    public GameObject FindClosestSnap()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Snap");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - GameObject.Find("Player").transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        timeCooldown -= Time.deltaTime;
        pullbackTimer -= Time.deltaTime;
        cooldownDisplay += Time.deltaTime;
        nextLevelCooldown -= Time.deltaTime;
        slider.value = cooldownDisplay;

        if (pullbackTimer < 0 && pullbacked == true)
        {
            // Pulls player back to current level
            // DestroyImmediate(lastLevel);
            LeanPool.Despawn(lastLevel);
            lastLevel = LeanPool.Spawn(levels[lastLevelIndex], instPos[lastLevelIndex], Quaternion.identity);
            pullbacked = false;
            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_pullback();

            Snap(GameObject.Find("Player"));

            InPresent();

            TimelineMovementEvent();

        }

        //if (timeCooldown < 0)
        //{
        //    travelStatusText.text = "Enabled";
        //    travelStatusText.color = new Color32(0, 255, 0, 255);
        //}
        //else
        //{
        //    travelStatusText.text = "Disabled";
        //    travelStatusText.color = new Color32(255, 0, 0, 255);
        //}

    }

    public void NextLevel()
    {
        if (nextLevelCooldown < 0)
        {
            if (currentLevel > levels.Length - 1)
            {
                GameObject.Find("MusicManager").GetComponent<MusicManager>().GoToCredits(); // Enable credits music
                SceneManager.LoadScene("credits");

            }
            else
            {
                // Remove old hints
                GameObject[] hints = GameObject.FindGameObjectsWithTag("hint");

                foreach (GameObject hint in hints)
                {
                    Destroy(hint);
                }

                Debug.Log(currentLevel);

                lastDoorPos = GameObject.Find("Player").transform.position;
                StartCoroutine("Flicker"); // Make screen flicker
                GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_timeTravel(); // Play time travel sound effect
                LeanPool.Despawn(lastLevel);
                lastLevel = LeanPool.Spawn(levels[currentLevel], instPos[currentLevel], Quaternion.identity);
                Debug.Log("Current level: " + currentLevel);
                if (currentLevel == 2)
                {
                    GameObject.Find("Keys").GetComponent<Fade>().FadeIn();
                }

                currentLevel++;
                TimelineMovementEvent();

            }
            nextLevelCooldown = 0.5F;
        }

    }

    private void TimeTravel()
    {
        if (PlayerPrefs.GetInt("TimeTravelHint") != 0)
        {
            PlayerPrefs.SetInt("TimeTravelHint", 0);
            StartCoroutine(Type(hintText, hint, 0.03F));
        }
    }

    IEnumerator Type(TextMeshProUGUI text, string textToType, float typingSpeed)
    {
        foreach (char letter in textToType.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(3);
        foreach (char letter in text.text.ToCharArray())
        {
            text.text = text.text.Remove(text.text.Length - 1); ;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private bool IntToBool(int input)
    {
        if (input == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    IEnumerator Flicker()
    {
        if (IntToBool(PlayerPrefs.GetInt("DisableFlashing")) == false)
        {
            flicker.SetActive(true);
            yield return new WaitForSeconds(0.09F);
            flicker.SetActive(false);
            yield return new WaitForSeconds(0.04F);
            flicker.SetActive(true);
            yield return new WaitForSeconds(0.02F);
            flicker.SetActive(false);
            yield return new WaitForSeconds(0.05F);
            flicker.SetActive(true);
            yield return new WaitForSeconds(0.03F);
            flicker.SetActive(false);
            yield return new WaitForSeconds(0.06F);
            flicker.SetActive(true);
            yield return new WaitForSeconds(0.07F);
            flicker.SetActive(false);
        }

    }

    private void TimelineMovementEvent()
    {

        GameObject[] portals = GameObject.FindGameObjectsWithTag("portal");
        Debug.Log("TME");

        foreach (GameObject portal in portals)
        {
            portal.GetComponent<Portal>().TimelineMovementEvent();
        }

        GameObject.Find("AQM").GetComponent<AudioQueue>().queuedPlayers.Clear(); // Remove queued players to prevent incorrect count
        for (int i = 0; i < keysPicked.Count; i++)
        {
            GameObject b = GameObject.Find(keysPicked[i]);
            if (b != null)
            {
                Destroy(b);
            }
        }

        for (int i = 0; i < buttonsActivated.Count; i++)
        {
            GameObject ba = GameObject.Find(buttonsActivated[i]);
            if (ba != null)
            {
                ba.GetComponent<cup>().F_on();
            }
        }

    }

    public void LastLevelPullback(float pullbackTime)
    {
        if (timeCooldown < 0 && currentLevel > 1 && pullbackTimer < 0)
        {
            StartCoroutine("Flicker"); // Make screen flicker
            //DestroyImmediate(lastLevel);
            LeanPool.Despawn(lastLevel);

            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_timeTravel(); // Play time travel sound effect


            // Weird mess of code
            lastLevelIndex = currentLevel - 1;
            lastLevel = LeanPool.Spawn(levels[currentLevel - 2],instPos[currentLevel - 2], Quaternion.identity);


            // Snap to nearest point
            Snap(GameObject.Find("Player"));

            InPast();

            if (GameObject.Find("Player").GetComponent<PlayerDataHolder>().Eternity == false)//Player with ability Eternity will not be pulled back 
            {
                // Stuff
                pullbackTimer = pullbackTime;
                pullbacked = true;
                timeCooldown = 3.5F;
                cooldownDisplay = 0;
            }
            TimelineMovementEvent();
            TimeTravel();
        }

    }


    public void NextLevelPullback(float pullbackTime)
    {
        if (timeCooldown < 0 && currentLevel < levels.Length && pullbackTimer < 0)
        {
            StartCoroutine("Flicker"); // Make screen flicker
            //DestroyImmediate(lastLevel);
            LeanPool.Despawn(lastLevel);

            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_timeTravel(); // Play time travel sound effect


            // Weird mess of code
            lastLevelIndex = currentLevel - 1;
            lastLevel = LeanPool.Spawn(levels[currentLevel], instPos[currentLevel], Quaternion.identity);
            // Snap to nearest point
            Snap(GameObject.Find("Player"));
            // Stuff

            InFuture();
            TimeTravel();

            if (GameObject.Find("Player").GetComponent<PlayerDataHolder>().Eternity == false)
            {
                pullbackTimer = pullbackTime;
                pullbacked = true;
                timeCooldown = 3.5F;
                cooldownDisplay = 0;

                TimelineMovementEvent();
            }
        }
    }
}

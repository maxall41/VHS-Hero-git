using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    private int currentLevel = 0;

    private float pullbackTimer;

    private GameObject lastLevel;

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

    private void Start()
    {
        lastLevel = Instantiate(levels[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);
        currentLevel++;
        playerStartPos = GameObject.Find("Player").transform.position;

    }

    public void Restart()
    {
        //TODO: Fix voice line system on restart
        GameObject.Find("Player").transform.parent = transHolder.transform;
        StartCoroutine("flicker");
        Destroy(lastLevel);
        lastLevel = Instantiate(levels[currentLevel - 1], new Vector3(0, 0, 0), Quaternion.identity);


        GameObject.Find("Player").transform.position = new Vector3(5, 3,0);


        // Reset saved parameters
        GameObject.Find("Player").GetComponent<PlayerDataHolder>().holdingKey = false;
        keysPicked.Clear();
        buttonsActivated.Clear();
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
            GameObject.Find("Player").transform.parent = lastLevel.transform;
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
        timeCooldown -= Time.deltaTime;
       pullbackTimer -= Time.deltaTime;
        cooldownDisplay += Time.deltaTime;
        nextLevelCooldown -= Time.deltaTime;
        slider.value = cooldownDisplay;

        if (pullbackTimer < 0 && pullbacked == true)
        {
            // Pulls player back to current level
            GameObject.Find("Player").transform.parent = transHolder.transform;
            DestroyImmediate(lastLevel);
            lastLevel = Instantiate(levels[lastLevelIndex], new Vector3(0,0, 0), Quaternion.identity);
            GameObject.Find("Player").transform.parent = lastLevel.transform;
            pullbacked = false;
            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_pullback();

            Snap(GameObject.Find("Player"));

            TimelineMovementEvent();

        }

        if (timeCooldown < 0)
        {
            travelStatusText.text = "Enabled";
            travelStatusText.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            travelStatusText.text = "Disabled";
            travelStatusText.color = new Color32(255, 0, 0, 255);
        }
        
    }

    public void NextLevel()
    {
        if (nextLevelCooldown < 0)
        {
            if (currentLevel > levels.Length - 1)
            {
                SceneManager.LoadScene("credits");

            }
            else
            {
                GameObject.Find("Player").transform.parent = transHolder.transform;
                Debug.Log(currentLevel);
                StartCoroutine("Flicker"); // Make screen flicker
                GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_timeTravel(); // Play time travel sound effect
                Destroy(lastLevel);
                lastLevel = Instantiate(levels[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);
                GameObject.Find("Player").transform.parent = lastLevel.transform;
                currentLevel++;
                TimelineMovementEvent();
                GameObject.Find("Tutorial").GetComponent<TextPlayer>().NextLevel();
            }
            nextLevelCooldown = 0.5F;
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
            
            GameObject.Find("AQM").GetComponent<AudioQueue>().queuedPlayers.Clear(); // Remove queued players to prevent incorrect count
            for (int i = 0;i < keysPicked.Count;i++)
            {
                GameObject b = GameObject.Find(keysPicked[i]);
                if (b != null)
                {
                    Destroy(b);
                }
            }

            for (int i = 0;i < buttonsActivated.Count;i++)
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
            if (timeCooldown < 0)
            {
                StartCoroutine("Flicker"); // Make screen flicker
            GameObject.Find("Player").transform.parent = transHolder.transform;
            DestroyImmediate(lastLevel);

                GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_timeTravel(); // Play time travel sound effect

                  
                 // Weird mess of code
                lastLevelIndex = currentLevel - 1;
                lastLevel = Instantiate(levels[currentLevel - 2], new Vector3(0, 0, 0), Quaternion.identity);
            GameObject.Find("Player").transform.parent = lastLevel.transform;
            // Snap to nearest point
            Snap(GameObject.Find("Player"));
                // Stuff
                pullbackTimer = pullbackTime;
                pullbacked = true;
                timeCooldown = 3.5F;
                cooldownDisplay = 0;
            TimelineMovementEvent();
            }
        }


    public void NextLevelPullback(float pullbackTime)
    {
        if (timeCooldown < 0)
        {
            StartCoroutine("Flicker"); // Make screen flicker
            GameObject.Find("Player").transform.parent = transHolder.transform;
            DestroyImmediate(lastLevel);

            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_timeTravel(); // Play time travel sound effect


            // Weird mess of code
            lastLevelIndex = currentLevel - 1;
            lastLevel = Instantiate(levels[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);
            GameObject.Find("Player").transform.parent = lastLevel.transform;
            // Snap to nearest point
            Snap(GameObject.Find("Player"));
            // Stuff
            pullbackTimer = pullbackTime;
            pullbacked = true;
            timeCooldown = 3.5F;
            cooldownDisplay = 0;
            TimelineMovementEvent();
        }
    }
}

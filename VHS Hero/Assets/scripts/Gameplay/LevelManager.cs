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

    private int lastLevelIndex;

    private float timeCooldown;

    private float cooldownDisplay = 3.5F;

    private bool onStart;

    public TextMeshProUGUI travelStatusText;

    public GameObject flicker;

    GameObject closetsObject;
    private float oldDistance = 9999;

    public Slider slider;

    private Vector3 playerStartPos;

    private void Start()
    {
        lastLevel = Instantiate(levels[currentLevel], new Vector3(0, 2.9F, 0), Quaternion.identity);
        currentLevel++;
        onStart = true;
        playerStartPos = GameObject.Find("Player").transform.position;

    }

    public void Restart()
    {
        //TODO: Fix:
        StartCoroutine("flicker");
        Destroy(lastLevel);
        lastLevel = Instantiate(levels[currentLevel], new Vector3(0, 2.9F, 0), Quaternion.identity);


        GameObject.Find("Player").transform.position = new Vector3(5, 3,0);
    }

    private void Snap(GameObject gb)
    {
        // Snap to nearest point to prevent player falling off map.

        GameObject[] NearGameobjects = GameObject.FindGameObjectsWithTag("Snap");
        foreach (GameObject g in NearGameobjects)
        {
            float dist = Vector3.Distance(GameObject.Find("Player").transform.position, g.transform.position);
            if (dist < oldDistance)
            {
                closetsObject = g;
                oldDistance = dist;
            }
        }
        gb.transform.position = new Vector3(closetsObject.transform.position.x, closetsObject.transform.position.y + 2, 0);
        //gb.transform.position = new Vector3(gb.transform.position.x, gb.transform.position.y, 0);
    }

    private void Update()
    {
        timeCooldown -= Time.deltaTime;
       pullbackTimer -= Time.deltaTime;
        cooldownDisplay += Time.deltaTime;
        slider.value = cooldownDisplay;

        if (pullbackTimer < 0 && pullbacked == true)
        {
            // Pulls player back to current level
            Destroy(lastLevel);
            lastLevel = Instantiate(levels[lastLevelIndex], new Vector3(0,2.9F, 0), Quaternion.identity);
            pullbacked = false;
            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_pullback();

            Snap(GameObject.Find("Player"));

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

        if (currentLevel > levels.Length - 1)
        {
            SceneManager.LoadScene("credits");

        } else
        {
            Debug.Log(currentLevel);
            StartCoroutine("Flicker"); // Make screen flicker
            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_timeTravel(); // Play time travel sound effect
            Destroy(lastLevel);
            lastLevel = Instantiate(levels[currentLevel], new Vector3(0, 2.9F, 0), Quaternion.identity);
            currentLevel++;
            onStart = false;
        }
    }

    IEnumerator Flicker()
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

        public void LastLevelPullback(float pullbackTime)
        {
            if (timeCooldown < 0)
            {
                StartCoroutine("Flicker"); // Make screen flicker
                Destroy(lastLevel); // Remove old level

                GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_timeTravel(); // Play time travel sound effect


                // Weird mess of code
                if (onStart == false)
                {
                    lastLevelIndex = currentLevel - 1;

                }
                else
                {
                    lastLevelIndex = 0;
                }
                lastLevel = Instantiate(levels[currentLevel - 2], new Vector3(0, 2.9F, 0), Quaternion.identity);
                // Snap to nearest point
                Snap(GameObject.Find("Player"));
                // Stuff
                pullbackTimer = pullbackTime;
                pullbacked = true;
                timeCooldown = 3.5F;
                cooldownDisplay = 0;
            }
        }


    public void NextLevelPullback(float pullbackTime)
    {
        if (timeCooldown < 0)
        {
            StartCoroutine("Flicker"); // Make screen flicker
            Destroy(lastLevel); // Remove old level

            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_timeTravel(); // Play time travel sound effect


            // Weird mess of code
            if (onStart == false)
            {
                lastLevelIndex = currentLevel - 1;

            } else
            {
                lastLevelIndex = 0;
            }
            lastLevel = Instantiate(levels[currentLevel], new Vector3(0, 2.9F, 0), Quaternion.identity);

            // Snap to nearest point
            Snap(GameObject.Find("Player"));
            // Stuff
            pullbackTimer = pullbackTime;
            pullbacked = true;
            timeCooldown = 3.5F;
            cooldownDisplay = 0;
        }
    }
}

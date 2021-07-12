using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Lean.Pool;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    private int currentLevel = 1;
    public int CurrentLevel { get => currentLevel; } // used to inactivate portals in past / future levels.
    private float pullbackTimer;

    private GameObject lastLevel;

    public GameObject keyUI;

    private bool pullbacked;

    public List<string> keysPicked = new List<string>();

    public List<string> buttonsActivated = new List<string>();

    private int lastLevelIndex;

    private float timeCooldown;

    public TextMeshProUGUI travelStatusText;

    public GameObject flicker;

    public GameObject transHolder;

    private float nextLevelCooldown; // Fixes issues with timing

    public Slider slider;

    private Vector3 playerStartPos;

    public GameObject Past;

    public GameObject Present;

    public GameObject Future;

    private Vector3 lastDoorPos;

    public TextMeshProUGUI hintText;

    public enum TemporalPosition { Past, Present, Future };

    public TemporalPosition currentTemporalPosition = TemporalPosition.Present; // Create a Selection object that will be used throughout script

    public string hint;

    public GameObject leftIndicator;

    public GameObject rightIndicator;

    public bool portalIndicatorEnabled = true;

    public sfxManager SFXManager;

    public MusicManager musicManager;

    public GameObject player;


    private void Start()
    {
        lastLevel = LeanPool.Spawn(levels[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);
        currentLevel++;
        playerStartPos = player.transform.position;

        SFXManager = GameObject.Find("SFX Manager").GetComponent<sfxManager>();

        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();

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
        currentTemporalPosition = TemporalPosition.Future;
    }

    private void InPast()
    {
        Present.SetActive(false);
        Future.SetActive(false);
        Past.SetActive(true);
        currentTemporalPosition = TemporalPosition.Past;

    }

    private void InPresent()
    {
        Future.SetActive(false);
        Past.SetActive(false);
        Present.SetActive(true);
        currentTemporalPosition = TemporalPosition.Present;
    }

    public void Restart()
    {
        keysPicked.Clear();
        buttonsActivated.Clear();
        StartCoroutine(FadeOutOfCurrentLevel());
        // Not using LeanPool because it causes issues with respawning objects
        Destroy(lastLevel);
        lastLevel = Instantiate(levels[currentLevel - 1], new Vector3(0, 0, 0), Quaternion.identity);


        player.transform.position = lastDoorPos;
           

        // Reset saved parameters
        player.GetComponent<PlayerDataHolder>().holdingKey = false;

        keyUI.SetActive(false);
    }




    private void Update()
    {

        timeCooldown -= Time.deltaTime;
        pullbackTimer -= Time.deltaTime;
        nextLevelCooldown -= Time.deltaTime;

        ShowPortalPositionIndicator();

        if (pullbackTimer < 0 && pullbacked == true)
        {
            Pullback();
        }

    }

    public void Pullback()
    {
        LeanPool.Despawn(lastLevel);
        lastLevel = LeanPool.Spawn(levels[lastLevelIndex], new Vector3(0, 0, 0), Quaternion.identity);
        pullbacked = false;
        SFXManager.F_pullback();

        InPresent();

        TimelineMovementEvent();
    }

    private void ShowPortalPositionIndicator()
    {
        Vector3 diff = player.transform.position - GameObject.FindGameObjectWithTag("portal").transform.position;

        if (portalIndicatorEnabled == true)
        {
            if (diff.x > 0)
            {
                leftIndicator.GetComponent<Image>().enabled = true;
                rightIndicator.GetComponent<Image>().enabled = false;
            }
            else
            {
                leftIndicator.GetComponent<Image>().enabled = false;
                rightIndicator.GetComponent<Image>().enabled = true;
            }
        } else
        {
            leftIndicator.GetComponent<Image>().enabled = false;
            rightIndicator.GetComponent<Image>().enabled = false;
        }

    }

    public void NextLevel()
    {
        if (nextLevelCooldown < 0)
        {
            if (currentLevel > levels.Length - 2)
            {
                musicManager.GoToCredits(); // Enable credits music
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

                lastDoorPos = player.transform.position;
                StartCoroutine(FadeOutOfCurrentLevel());
                SFXManager.F_timeTravel(); // Play time travel sound effect
                LeanPool.Despawn(lastLevel);
                lastLevel = LeanPool.Spawn(levels[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);
                if (currentLevel == 3)
                {
                    GameObject.Find("BackwardsTip").GetComponent<Fade>().FadeIn();
                }

                else if (currentLevel == 4)
                {
                    GameObject.Find("ForwardsTip").GetComponent<Fade>().FadeIn();
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

    IEnumerator FadeOutOfCurrentLevel()
    {
        float duration = 0.65F; // duration of transition
        float targetValueCA = 1; // Target for transition (FADE IN)
        float targetValueBlur = 0.8F; // Target for transition (FADE IN)
        //if (IntToBool(PlayerPrefs.GetInt("DisableFlashing")) == false)
        //{
        //    flicker.SetActive(true);
        //    yield return new WaitForSeconds(0.09F);
        //    flicker.SetActive(false);
        //    yield return new WaitForSeconds(0.04F);
        //    flicker.SetActive(true);
        //    yield return new WaitForSeconds(0.02F);
        //    flicker.SetActive(false);
        //    yield return new WaitForSeconds(0.05F);
        //    flicker.SetActive(true);
        //    yield return new WaitForSeconds(0.03F);
        //    flicker.SetActive(false);
        //    yield return new WaitForSeconds(0.06F);
        //    flicker.SetActive(true);
        //    yield return new WaitForSeconds(0.07F);
        //    flicker.SetActive(false);
        //}

        float currentTime = 0;
        ClampedFloatParameter currentValCA = new ClampedFloatParameter(0, 0, 1, false);
        MinFloatParameter currentValBlur = new MinFloatParameter(0, 0);
        var volume = GameObject.Find("transitionEffect").GetComponent<Volume>();
        if (volume.profile.TryGet<ChromaticAberration>(out var ca) == false)
        {
            Debug.LogError("Issue getting pp effect (ChromaticAbberation)");
        }

        if (volume.profile.TryGet<DepthOfField>(out var blur) == false)
        {
            Debug.LogError("Issue getting pp effect (BLUR)");
        }

        currentValCA = ca.intensity;
        currentValBlur = blur.focusDistance;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newValCA = Mathf.Lerp(currentValCA.value, targetValueCA, currentTime / duration);
            ca.intensity.overrideState = true;
            ca.intensity.Override(newValCA);

            float newValBlur = Mathf.Lerp(currentValBlur.value, targetValueBlur, currentTime / duration);
            blur.focusDistance.overrideState = true;
            blur.focusDistance.Override(newValBlur);

            yield return null;
        }

        StartCoroutine(FadeIntoNewLevel());



    }

    IEnumerator FadeIntoNewLevel() {

        float duration = 0.65F; // duration of transition
        float targetValueCA = 0; // Target for transition (FADE OUT)
        float targetValueBlur = 3; // Target for transition (FADE OUT)


        float currentTime = 0;
        ClampedFloatParameter currentValCA = new ClampedFloatParameter(0, 0, 1, false);
        MinFloatParameter currentValBlur = new MinFloatParameter(0, 0);
        var volume = GameObject.Find("transitionEffect").GetComponent<Volume>();
        if (volume.profile.TryGet<ChromaticAberration>(out var ca) == false)
        {
            Debug.LogError("Issue getting pp effect (ChromaticAbberation)");
        }

        if (volume.profile.TryGet<DepthOfField>(out var blur) == false)
        {
            Debug.LogError("Issue getting pp effect (BLUR)");
        }

        currentValCA = ca.intensity;
        currentValBlur = blur.focusDistance;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newValCA = Mathf.Lerp(currentValCA.value, targetValueCA, currentTime / duration);
            ca.intensity.overrideState = true;
            ca.intensity.Override(newValCA);

            float newValBlur = Mathf.Lerp(currentValBlur.value, targetValueBlur, currentTime / duration);
            blur.focusDistance.overrideState = true;
            blur.focusDistance.Override(newValBlur);

            yield return null;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(FadeIntoNewLevel());
    }




    private void TimelineMovementEvent()
    {

        GameObject[] portals = GameObject.FindGameObjectsWithTag("portal");

        foreach (GameObject portal in portals)
        {
            portal.GetComponent<Portal>().TimelineMovementEvent();
        }

        for (int i = 0; i < keysPicked.Count; i++)
        {
            GameObject b = GameObject.Find(keysPicked[i]);
            if (b != null)
            {
                //Destroy(b);
                b.GetComponent<Key>().enabled = false;
                b.GetComponent<SpriteRenderer>().enabled = false;
                b.GetComponent<BoxCollider2D>().enabled = false;
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

        GameObject[] keys = GameObject.FindGameObjectsWithTag("key");

        foreach (GameObject key in keys)
        {
            if (keysPicked.Contains(key.name) == false)
            {
                key.GetComponent<Key>().enabled = true;
                key.GetComponent<SpriteRenderer>().enabled = true;
                key.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

    }

    public void LastLevelPullback(float pullbackTime)
    {
        if (pullbackTimer < 0)
        {
            if (timeCooldown < 0 && currentLevel > 1)
            {
                StartCoroutine(FadeOutOfCurrentLevel());
   
                LeanPool.Despawn(lastLevel);

                SFXManager.F_timeTravel(); // Play time travel sound effect


                // Weird mess of code
                lastLevelIndex = currentLevel - 1;
                lastLevel = LeanPool.Spawn(levels[currentLevel - 2], new Vector3(0, 0, 0), Quaternion.identity);


                InPast();

                pullbackTimer = pullbackTime;
                pullbacked = true;
                timeCooldown = 3.5F;
                
                TimelineMovementEvent();
                TimeTravel();
            }
        }

    }


    public void NextLevelPullback(float pullbackTime)
    {
        if (pullbackTimer < 0)
        {
            if (timeCooldown < 0 && currentLevel < levels.Length)
            {
                StartCoroutine(FadeOutOfCurrentLevel());
                //DestroyImmediate(lastLevel);
                LeanPool.Despawn(lastLevel);

                SFXManager.F_timeTravel(); // Play time travel sound effect


                // Weird mess of code
                lastLevelIndex = currentLevel - 1;
                lastLevel = LeanPool.Spawn(levels[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);

                // Stuff

                InFuture();
                TimeTravel();

                pullbackTimer = pullbackTime;
                pullbacked = true;
                timeCooldown = 3.5F;

                TimelineMovementEvent();

            }
        }
    }
}

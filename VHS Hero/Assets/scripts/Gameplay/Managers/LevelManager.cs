using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Lean.Pool;


//TODO: Split into 2 scripts one for level manager and one for the time manager.

public class LevelManager : MonoBehaviour
{

    public GameObject[] levels; // Stores all levels in array as prefabs

    private int currentLevel = 1; // Stores index of current level

    public int CurrentLevel { get => currentLevel; } // used to inactivate portals in past / future levels.

    private float pullbackTimer; // Timer used to check when we need to pull the player back

    private GameObject lastLevel; //  Stores reference to last level so it can be destroyed when creating new levels

    public GameObject keyUI; // Stores reference to key symbol in the UI for adjustments during time travel

    private bool pullbacked; // Used to check if we should listen to pullbackTimer


    // Used to store if buttons or keys have been picked up at any temporal position.
    public List<string> keysPicked = new List<string>();

    public List<string> buttonsActivated = new List<string>();

    private int lastLevelIndex;

    private float timeCooldown; // Time travel cooldown timer

    private float nextLevelCooldown; // Fixes issues with timing


    // Post processing volumes
    public GameObject Past;

    public GameObject Present;

    public GameObject Future;

    private Vector3 lastDoorPos; // Stores position of last door used when reseting player 

    public TextMeshProUGUI hintText; // hint text used when the time travel hint is displayed

    // Stores current temporal position
    public enum TemporalPosition { Past, Present, Future };

    public TemporalPosition currentTemporalPosition = TemporalPosition.Present;


    // Managers
    public sfxManager SFXManager; // Used to play sound effects

    public MusicManager musicManager; // Used to play music when changing scenes

    public GameObject player; // Stores refernce for player used when resetting player's position

    private float pullbackTimeG; // Dark magic

    public HintsReference hint; // Reference to hint settings


    private void Start()
    {
        lastLevel = LeanPool.Spawn(levels[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);
        currentLevel++;

        GameObject.Find("RestartTip").GetComponent<Fade>().FadeIn();

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
        Future.GetComponent<Volume>().weight = 1;
        Past.GetComponent<Volume>().weight = 0;
        Present.GetComponent<Volume>().weight = 0;
        currentTemporalPosition = TemporalPosition.Future;
    }

    private void InPast()
    {
        Future.GetComponent<Volume>().weight = 0;
        Past.GetComponent<Volume>().weight = 1;
        Present.GetComponent<Volume>().weight = 0;
        currentTemporalPosition = TemporalPosition.Past;

    }

    private void InPresent()
    {
        Future.GetComponent<Volume>().weight = 0;
        Past.GetComponent<Volume>().weight = 0;
        Present.GetComponent<Volume>().weight = 1;
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


        player.transform.position = lastDoorPos; // Reset player position to last door
           

        // Reset saved parameters
        player.GetComponent<PlayerDataHolder>().holdingKey = false;

        keyUI.SetActive(false);
    }




    private void Update()
    {
        // Update timers
        timeCooldown -= Time.deltaTime;
        pullbackTimer -= Time.deltaTime;
        nextLevelCooldown -= Time.deltaTime;

            
        // Check if we need to pull the player back to their current ref frame.
        if (pullbackTimer < 0 && pullbacked == true)
        {
            Pullback();
        }

        // Post processing fade back effect
        if (pullbackTimer > 0)
        {
            Present.GetComponent<Volume>().weight += (Time.deltaTime / pullbackTimeG);
            Past.GetComponent<Volume>().weight -= (Time.deltaTime / pullbackTimeG);
            Future.GetComponent<Volume>().weight -= (Time.deltaTime / pullbackTimeG);
        }

    }

    public void Pullback()
    {
        LeanPool.Despawn(lastLevel);
        lastLevel = LeanPool.Spawn(levels[lastLevelIndex], new Vector3(0, 0, 0), Quaternion.identity);
        pullbacked = false;
        SFXManager.F_pullback();

        StartCoroutine(FadeOutOfCurrentLevel());

        InPresent();

        TimelineMovementEvent();
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

                lastDoorPos = player.transform.position; // Used when player restarts level

                StartCoroutine(FadeOutOfCurrentLevel()); // Play transition effect

                SFXManager.F_timeTravel(); // Play time travel sound effect

                // Load new level
                LeanPool.Despawn(lastLevel);
                lastLevel = LeanPool.Spawn(levels[currentLevel], new Vector3(0, 0, 0), Quaternion.identity);


                // Play tips if they are applicable.
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
        // Plays time travel hint
        if (PlayerPrefs.GetInt("TimeTravelHint") != 0)
        {
            PlayerPrefs.SetInt("TimeTravelHint", 0);
            StartCoroutine(Type(hintText, hint.hints["TimeTravelHint"], 0.03F));
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
        // Triggers portal with update on their temporal status

        GameObject[] portals = GameObject.FindGameObjectsWithTag("portal");

        foreach (GameObject portal in portals)
        {
            portal.GetComponent<Portal>().TimelineMovementEvent();
        }

        // Updates keys positions and existence

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

        // Updates buttons positions and existence

        for (int i = 0; i < buttonsActivated.Count; i++)
        {
            GameObject ba = GameObject.Find(buttonsActivated[i]);
            if (ba != null)
            {
                ba.GetComponent<cup>().F_on();
            }
        }

        // Dark magic (I forgot what this does and it is very complicated)

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
                pullbackTimeG = pullbackTime;
                
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
                pullbackTimeG = pullbackTime;

                TimelineMovementEvent();

            }
        }
    }
}

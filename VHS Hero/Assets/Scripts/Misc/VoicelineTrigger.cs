using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VoicelineTrigger : MonoBehaviour
{

    public bool playedBefore = false;

    public string id;

    public AudioSource audioSource;

    public bool checkingForFinish;

    public bool keyPopup = false;

    public GameObject key;

    private Transform keyOrigin;

    private float triggerCooldown = -100;


    public void play()
    {
        Debug.Log("play");
        audioSource.Play();
        checkingForFinish = true;

        //if (keyPopup == true)
        //{
        //    keyOrigin = key.transform;
        //    LeanTween.moveY(key, GameObject.Find("KeyPopupPoint").transform.position.y, 1F).setEase(LeanTweenType.easeInOutBounce);
        //    StartCoroutine(keyLeave());
        //}


    }

    private IEnumerator keyLeave()
    {
        Debug.Log("KEY LEAVE");
        yield return new WaitForSeconds(3);
        LeanTween.moveY(key, GameObject.Find("KeyPopupPoint").transform.position.y - 100, 1F).setEase(LeanTweenType.easeInOutBounce);
        Debug.Log("OUT");
    }

    private void Update()
    {
        triggerCooldown -= Time.deltaTime;
        if (audioSource.isPlaying == false && checkingForFinish == true)
        {
            Debug.Log("Finished call from voiceline trigger " + gameObject.name);
            GameObject.Find("AQM").GetComponent<AudioQueue>().Finished();
            checkingForFinish = false;
        }
    }

    private void Start()
    {
        gameObject.name = id;
        gameObject.transform.parent = GameObject.Find("AudioPlayHolder").transform;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (playedBefore == false && triggerCooldown < 0)
        {
            triggerCooldown = 0.5F;
            playedBefore = true;
            Debug.Log("Queued");
            GameObject.Find("AQM").GetComponent<AudioQueue>().AddToQueue(this);

        }
     }
}


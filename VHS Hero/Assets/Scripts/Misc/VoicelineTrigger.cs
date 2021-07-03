using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VoicelineTrigger : MonoBehaviour
{

    public bool OnlyPlayOnce = true;

    private bool playedBefore = false;

    public string id;

    public AudioSource audioSource;

    private bool checkingForFinish;


    public void play()
    {
        Debug.Log("play");
        audioSource.Play();
        checkingForFinish = true;
    }

    private void Update()
    {
        if (audioSource.isPlaying == false && checkingForFinish == true)
        {
            Debug.Log("Finished call from voiceline trigger " + gameObject.name);
            GameObject.Find("AQM").GetComponent<AudioQueue>().Finished();
            checkingForFinish = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (OnlyPlayOnce == false)
        {
            audioSource.Play();
        } else
        {
            //if (playedBefore == false && PlayerPrefs.GetInt(id) == 0)
            if (playedBefore == false)
            {
                Debug.Log("Added");
                playedBefore = true;
                GameObject.Find("AQM").GetComponent<AudioQueue>().AddToQueue(this);

                PlayerPrefs.SetInt(id, 1);

            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MusicManager : MonoBehaviour
{

    
    public AudioSource musicSource;

    public AudioClip defaultGameplayMusic;

    public AudioClip settingsMusic;

    public AudioClip creditsMusic;

    public AudioClip menuMusic;

    public AudioMixer musicMixer;



    public void GoToGameplay()
    {
        StartCoroutine(StartFade(musicMixer, "MusicVol", 1, 0, "Gameplay"));
    }

    public void GoToCredits()
    {
        StartCoroutine(StartFade(musicMixer, "MusicVol", 1, 0, "credits"));
    }

    public void GoToMenu()
    {
        StartCoroutine(StartFade(musicMixer, "MusicVol", 1, 0, "menu"));
    }


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (GameObject.FindGameObjectsWithTag("MusicManager").Length > 1)
        {
            Destroy(gameObject);
        }

    }

    private void FinishedAudioFade(string targetScene)
    {

        if (targetScene == "Gameplay")
        {
            musicSource.clip = defaultGameplayMusic;
            StartCoroutine(StartFade(musicMixer, "MusicVol", 1, 1, "None"));
            musicSource.Play();
        }

        if (targetScene == "credits")
        {
            musicSource.clip = creditsMusic;
            StartCoroutine(StartFade(musicMixer, "MusicVol", 1, 1, "None"));
            musicSource.Play();
        }

        if (targetScene == "menu")
        {
            musicSource.clip = menuMusic;
            StartCoroutine(StartFade(musicMixer, "MusicVol", 1, 1, "None"));
            musicSource.Play();
        }
    }

    public IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume, string targetScene)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        FinishedAudioFade(targetScene);
        yield break;
    }
}

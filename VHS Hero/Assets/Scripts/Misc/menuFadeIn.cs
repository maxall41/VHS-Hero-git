using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class menuFadeIn : MonoBehaviour
{
    public GameObject light;
    public AudioMixer musicMixer;


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("fadeInMenu") == 1)
        {
            GameObject.Find("MusicManager").GetComponent<AudioSource>().enabled = false;
            StartCoroutine(FadeOut());
        } else
        {
            gameObject.GetComponent<CanvasGroup>().alpha = 0;
            light.SetActive(true);
        }
           
    }

    public void done()
    {
        musicMixer.SetFloat("MusicVol", 0);
        light.SetActive(true);
        GameObject.Find("MusicManager").GetComponent<AudioSource>().enabled = true;
        StartCoroutine(StartFade(musicMixer, "MusicVol", 5, 1));
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(1);
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0, 1).setOnComplete(done);

        PlayerPrefs.SetInt("fadeInMenu", 0);
    }

    public IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
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
        yield break;
    }

}

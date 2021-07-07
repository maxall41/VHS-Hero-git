using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public GameObject loadingScreen;

    List<AsyncOperation> scenesLoading;

    public void Play()
    {
        //SceneManager.LoadScene("main");
        //loadingScreen.SetActive(true);
        //StartCoroutine(StartFade(musicMixer, "MusicVol", 1, 0,"main"));
        GameObject.Find("MusicManager").GetComponent<MusicManager>().GoToGameplay();
        scenesLoading.Add(SceneManager.LoadSceneAsync("main"));
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
}
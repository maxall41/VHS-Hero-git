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
        GameObject.Find("MusicManager").GetComponent<MusicManager>().GoToGameplay();
        SceneManager.LoadSceneAsync("main");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void GoHome()
    {
        SceneManager.LoadScene("menu");
        GameObject.Find("MusicManager").GetComponent<MusicManager>().GoToMenu();
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        GameObject.Find("MusicManager").GetComponent<MusicManager>().GoToCredits();
    }
}
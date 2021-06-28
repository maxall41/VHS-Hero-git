using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("main");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void About()
    {
        SceneManager.LoadScene("About");
    }
}
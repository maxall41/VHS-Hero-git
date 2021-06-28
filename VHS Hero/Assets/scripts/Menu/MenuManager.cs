using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public GameObject loadingScreen;

    List<AsyncOperation> scenesLoading;
    public void Play()
    {
        //SceneManager.LoadScene("main");
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.LoadSceneAsync("main"));

        StartCoroutine(GetSceneLoadProgress());
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void About()
    {
        SceneManager.LoadScene("About");
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0;i < scenesLoading.Count;i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        loadingScreen.SetActive(false);
    }
}
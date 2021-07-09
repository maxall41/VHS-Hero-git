using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("fadeInMenu", 1);
        StartCoroutine(FadeIn_c());
    }

    private IEnumerator FadeIn_c()
    {
        yield return new WaitForSeconds(time);
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1, 1).setOnComplete(done);
    }

    private void done()
    {
        SceneManager.LoadSceneAsync("menu");
    }
}

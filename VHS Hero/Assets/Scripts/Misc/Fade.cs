using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

    public void FadeIn()
    {
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 1, 1);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(3);
        LeanTween.alphaCanvas(gameObject.GetComponent<CanvasGroup>(), 0, 1);
    }

}

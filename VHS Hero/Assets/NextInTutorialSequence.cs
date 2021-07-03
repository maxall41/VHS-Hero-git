using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextInTutorialSequence : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.Find("Tutorial").GetComponent<Tutorial>().NextSentence();
    }
}

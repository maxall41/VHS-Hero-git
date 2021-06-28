using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToThePast : MonoBehaviour
{
    public string doorName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("ILPM").GetComponent<InterLevelEffectManager>().doorNamesForInterLevelCauseAndEffect[GameObject.Find("ILPM").GetComponent<InterLevelEffectManager>().doorNamesForInterLevelCauseAndEffect.Length + 1] = doorName;
        }
    }
}

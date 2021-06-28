using UnityEngine;
using System.Collections;

public class InterLevelEffectManager : MonoBehaviour
{
    public string[] doorNamesForInterLevelCauseAndEffect;


    public void registerPullbackEffects()
    {
        for (int i = 0;i < doorNamesForInterLevelCauseAndEffect.Length;i++)
        {
            GameObject.Find(doorNamesForInterLevelCauseAndEffect[i]).GetComponent<Door>().open();
        }
    }
}

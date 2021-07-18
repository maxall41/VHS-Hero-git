using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsLoader : MonoBehaviour
{
    public string[] exposedVolParameters;
    public AudioMixer[] mixers;

    void Start()
    {
        for (int i = 0;i < exposedVolParameters.Length;i++)
        {
            mixers[i].SetFloat(exposedVolParameters[i], Mathf.Log10(PlayerPrefs.GetFloat(exposedVolParameters[i]) * 20));
        }

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualityIndex"));
    }
}

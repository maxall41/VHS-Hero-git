using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider slider;

    public float defaultValue;

    public string exposedVol;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(exposedVol, defaultValue);
    }
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(exposedVol, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(exposedVol, sliderValue);
    }
}
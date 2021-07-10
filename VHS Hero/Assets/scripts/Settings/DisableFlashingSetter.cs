using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableFlashingSetter : MonoBehaviour
{
    public void UpdateSetting(bool newSetting) {
        Debug.Log(newSetting);
        PlayerPrefs.SetInt("DisableFlashing", BoolToInt(newSetting));
        Debug.Log(BoolToInt(newSetting));
    }


    private int BoolToInt(bool input)
    {
        if (input == false)
        {
            return 0;
        } else
        {
            return 1;
        }
    }

    private bool IntToBool(int input)
    {
        if (input == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Start()
    {
        Debug.Log(IntToBool(PlayerPrefs.GetInt("DisableFlashing")));
        gameObject.GetComponent<Toggle>().isOn = IntToBool(PlayerPrefs.GetInt("DisableFlashing"));
    }

}

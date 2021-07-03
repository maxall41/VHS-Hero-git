using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxOnClick : MonoBehaviour
{
    public void click()
    {
        GetComponent<AudioSource>().Play();
    }
}

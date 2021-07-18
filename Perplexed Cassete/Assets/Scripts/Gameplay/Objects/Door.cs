using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void open()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 1);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public void open()
    {
        //LeanTween.moveLocalY(gameObject,transform.position.y + 1,1);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 1);
    }


}

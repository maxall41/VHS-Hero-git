using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameObject.Find("levelman").GetComponent<LevelManager>().Restart();
        }
    }
}

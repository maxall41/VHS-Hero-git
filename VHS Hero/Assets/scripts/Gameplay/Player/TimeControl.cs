using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    private float timeHeld;
    private float timeHeld2;
    public float pullBackAfterSeconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            timeHeld += Time.deltaTime;
        } else
        {
            timeHeld = 0;
        }

        if (timeHeld > 1)
        {
            GameObject.Find("levelman").GetComponent<LevelManager>().NextLevelPullback(pullBackAfterSeconds);
        }

        if (Input.GetKey(KeyCode.B))
        {
            timeHeld2 += Time.deltaTime;
        }
        else
        {
            timeHeld2 = 0;
        }

        if (timeHeld2 > 1)
        {
            GameObject.Find("levelman").GetComponent<LevelManager>().LastLevelPullback(pullBackAfterSeconds);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    private float timeHeld;
    private float timeHeld2;
    private float timeHeld3;
    public float pullBackAfterSeconds;

    private LevelManager levelman;
    // Start is called before the first frame update
    void Start()
    {
        levelman = GameObject.Find("levelman").GetComponent<LevelManager>();
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

        if (timeHeld > 0.3F) {
            if (levelman.currentTemporalPosition == LevelManager.TemporalPosition.Present)
            {
                levelman.GetComponent<LevelManager>().NextLevelPullback(pullBackAfterSeconds);
            } else if (levelman.currentTemporalPosition == LevelManager.TemporalPosition.Past)
            {
                levelman.Pullback();
            }
        }

        if (Input.GetKey(KeyCode.B))
        {
            timeHeld2 += Time.deltaTime;
        }
        else
        {
            timeHeld2 = 0;
        }

        if (timeHeld2 > 0.3F)
        {
            if (levelman.currentTemporalPosition == LevelManager.TemporalPosition.Present)
            {
                levelman.GetComponent<LevelManager>().LastLevelPullback(pullBackAfterSeconds);
            }
            else if (levelman.currentTemporalPosition == LevelManager.TemporalPosition.Future)
            {
                levelman.Pullback();
            }
        }


        //explode!
        if (Input.GetKey(KeyCode.R))
        {
            timeHeld3 += Time.deltaTime;
        }
        else
        {
            timeHeld3 = 0;
        }

        if (timeHeld3 > 0.3F)
        {
            levelman.GetComponent<LevelManager>().Restart();
        }

    }
}

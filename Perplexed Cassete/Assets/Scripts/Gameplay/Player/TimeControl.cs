using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeControl : MonoBehaviour
{
    private float timeHeld;
    private float timeHeld2;
    private float timeHeld3;
    public float pullBackAfterSeconds;

    public InputAction forwards;

    public InputAction backwards;

    public InputAction restart;

    private LevelManager levelman;
    // Start is called before the first frame update
    void Start()
    {
        levelman = GameObject.Find("levelman").GetComponent<LevelManager>();
    }

    private void OnEnable()
    {
        forwards.Enable();
        backwards.Enable();
        restart.Enable();
    }

    private void OnDisable()
    {
        forwards.Disable();
        backwards.Disable();
        restart.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (forwards.triggered)
        {
            if (levelman.currentTemporalPosition == LevelManager.TemporalPosition.Present)
            {
                levelman.GetComponent<LevelManager>().NextLevelPullback(pullBackAfterSeconds);
            }
            else if (levelman.currentTemporalPosition == LevelManager.TemporalPosition.Past)
            {
                levelman.Pullback();
            }
        }

        if (backwards.triggered)
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

        if (restart.triggered)
        {
            levelman.GetComponent<LevelManager>().Restart();
        }

    }
}

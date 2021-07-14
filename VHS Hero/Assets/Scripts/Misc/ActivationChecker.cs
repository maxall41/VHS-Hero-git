using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationChecker : MonoBehaviour
{
    public enum ActivationMode { SpriteRenderer, GameObject };

    public enum CheckingMode { Continous, OnStart };

    public string checkID;

    public int triggerState;

    public CheckingMode currentCheckingMode;

    public ActivationMode currentActivationMode;

    public bool activationFinalState;



    // Start is called before the first frame update
    void Start()
    {
        if (currentCheckingMode == CheckingMode.OnStart)
        {
            check();
        }
    }

    private void check()
    {
        if (PlayerPrefs.GetInt(checkID) == triggerState)
        {
            if (currentActivationMode == ActivationMode.SpriteRenderer)
            {
                GetComponent<SpriteRenderer>().enabled = activationFinalState;
            }
            else if (currentActivationMode == ActivationMode.GameObject)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (currentActivationMode == ActivationMode.SpriteRenderer)
            {
                GetComponent<SpriteRenderer>().enabled = !activationFinalState;
            }
            else if (currentActivationMode == ActivationMode.GameObject)
            {
                gameObject.SetActive(!activationFinalState);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCheckingMode == CheckingMode.Continous)
        {
            check();
        }
    }
}

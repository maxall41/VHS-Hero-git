using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cup : MonoBehaviour
{

    public Material offMat;
    public Material onMat;

    public bool on = false;

    public GameObject Knob;

    public List<string> tba = new List<string>();

    private void Start()
    {
        Knob = GameObject.Find("RefHolder").GetComponent<RefHolder>().knob;
    }

    public void inserted()
    {
        Debug.Log("Inserted");
        gameObject.GetComponent<Renderer>().material = onMat;
        tba = GameObject.Find("levelman").GetComponent<LevelManager>().buttonsActivated;
        on = true;

        tba.Add(gameObject.name);

        Knob.SetActive(false);
    }

    public void F_on()
    {
        Debug.Log("F_ON");
        gameObject.GetComponent<Renderer>().material = onMat;
        on = true;
        Knob.SetActive(false);
    }
}

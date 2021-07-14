using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;
public class TheGate : MonoBehaviour
{
    private bool WatchForE = false;
    public InputAction continueInput;
    private int activeArtifacts;
    public TextMeshProUGUI text;

    void OnTriggerExit2D(Collider2D other)
    {
        WatchForE = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        WatchForE = true;
    }

    private void OnEnable()
    {
        continueInput.Enable();
    }

    private void OnDisable()
    {
        continueInput.Disable();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("Nigredo") == 1)
        {
            activeArtifacts++;
        }
        if (PlayerPrefs.GetInt("Albedo") == 1)
        {
            activeArtifacts++;

        }
        if (PlayerPrefs.GetInt("Citrinitas") == 1)
        {
            activeArtifacts++;
        }

        if (activeArtifacts != 3)
        {
            text.text = activeArtifacts.ToString() + " / " + "3";
        }


    }

    private void Update()
    {
        if (WatchForE == true && continueInput.triggered)
        {

            if (PlayerPrefs.GetInt("Nigredo") == 1 && PlayerPrefs.GetInt("Albedo") == 1 && PlayerPrefs.GetInt("Citrinitas") == 1)
            {
                PlayerPrefs.SetInt("Freedom", 1);
                SceneManager.LoadScene("completeHidden");
            }
        }
    }
}

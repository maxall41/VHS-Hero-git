using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class TheGate : MonoBehaviour
{
    private bool WatchForE = false;
    public InputAction continueInput;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TheGate : MonoBehaviour
{
    private bool WatchForE = false;
    void OnTriggerExit2D(Collider2D other)
    {
        WatchForE = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        WatchForE = true;
    }

    private void Update()
    {
        if (WatchForE == true && Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerPrefs.GetInt("Nigredo") == 1 && PlayerPrefs.GetInt("Albedo") == 1 && PlayerPrefs.GetInt("Citrinitas") == 1)
            {
                PlayerPrefs.SetInt("Freedom", 1);
                SceneManager.LoadScene("completeHidden");
            }
        }
    }
}

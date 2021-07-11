using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheGate : MonoBehaviour
{
    private bool gateOpened = false;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
        
    {
       
        if (GameObject.Find("Player").GetComponent<PlayerDataHolder>().Nigredo == true 
            && GameObject.Find("Player").GetComponent<PlayerDataHolder>().Albedo == true
            && GameObject.Find("Player").GetComponent<PlayerDataHolder>().Citrinitas == true
            && PlayerPrefs.GetInt("Rubedo") == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            gateOpened = true;
        }

      
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gateOpened == true)
        {
            if (Input.GetKey(KeyCode.E))
            {
          
                SceneManager.LoadScene("completeHidden");
                PlayerPrefs.SetInt("Freedom", 1);
            }
        }
        

    }


}

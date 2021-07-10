using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPrompt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void next()
    {
        GameObject.Find("levelman").GetComponent<LevelManager>().NextLevel();
    }
}

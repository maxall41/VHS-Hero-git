using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingDisplay : MonoBehaviour
{

    public Sprite sprite;

    public SpriteRenderer sr;

    public string id;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(id) != 0)
        {
            sr.sprite = sprite;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door[] doorsToOpen;
    public bool DestroySelfOnCompletion = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            for (int i = 0;i < doorsToOpen.Length;i++)
            {
                doorsToOpen[i].open();
            }
            if (DestroySelfOnCompletion == true)
            {
                Destroy(gameObject);
            }
        }
    }
}

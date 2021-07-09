using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfInGround : MonoBehaviour
{
    public bool inGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            inGround = false;
        }
    }
}

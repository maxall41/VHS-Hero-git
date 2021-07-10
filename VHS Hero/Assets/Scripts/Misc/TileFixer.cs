using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFixer : MonoBehaviour
{
    public checkIfInGround[] checks;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Debug.Log("GROUND COLLIDE!!!");
            foreach (checkIfInGround check in checks)
            {
                Debug.Log("NAME: " + check.gameObject.name);
                if (check.inGround == false)
                {
                    Debug.Log("moving player");
                    gameObject.transform.parent.position = check.gameObject.transform.position;
                }
            }
        }
    }


}

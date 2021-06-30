using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHolder : MonoBehaviour
{
    public bool holdingBall = false;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "cup")
    //    {
    //        if (holdingBall == true)
    //        {
    //            Debug.Log("Press E to insert ball");


    //            if (Input.GetKeyDown(KeyCode.E))
    //            {
    //                Debug.Log("Inserted ball");
    //                holdingBall = false;
    //                collision.gameObject.GetComponent<cup>().inserted();
    //            }

    //        }
    //    }
    //}
    //TODO: Cleanup this horrible unperformant mess
    private void Update()
    {
        // Don't ask
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("cup");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - GameObject.Find("Player").transform.position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        if (distance < 6)
        {
            if (holdingBall == true)
            {
                Debug.Log("Press E to insert ball");


                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Inserted ball");
                    holdingBall = false;
                    closest.GetComponent<cup>().inserted();
                }

            }
        }
    }
}

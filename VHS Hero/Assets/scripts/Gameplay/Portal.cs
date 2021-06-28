using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour
{

    public GameObject levelManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("levelman").GetComponent<LevelManager>().NextLevel();
        }
    }
}

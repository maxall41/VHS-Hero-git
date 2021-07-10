using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityClimb : MonoBehaviour
{

    private bool watchForPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDataHolder>().ClimbWall = true;
            GameObject.Find("SFX Manager").GetComponent<sfxManager>().F_ability();
            Destroy(gameObject);

        }
    }

}

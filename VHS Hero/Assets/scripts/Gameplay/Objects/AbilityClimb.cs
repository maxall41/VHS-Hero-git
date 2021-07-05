using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityClimb : MonoBehaviour
{

    private bool watchForPickup;
    EffectOnPick effect = new EffectOnPick();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            watchForPickup = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            watchForPickup = false;
        }
    }


    private void Update()
    {
        if (watchForPickup == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("Player").GetComponent<PlayerDataHolder>().ClimbWall = true;
                effect.PickAbility();
                Destroy(gameObject);
            }
        }
    }
}

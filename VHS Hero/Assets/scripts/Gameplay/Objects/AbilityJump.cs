using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityJump : MonoBehaviour
{
    // Start is called before the first frame update

    private bool watchForPickup;
    EffectOnPick effect = new EffectOnPick();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerDataHolder>().DoubleJump = true;
            effect.PickAbility();
            Destroy(gameObject);

        }
    }

}

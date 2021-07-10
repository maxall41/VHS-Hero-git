using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbCheck : MonoBehaviour
{

    // Start is called before the first frame update
    Vector2 vector2 = new Vector2(0, 0);


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (this.gameObject.GetComponentInParent<PlayerDataHolder>().ClimbWall == true && this.gameObject.GetComponentInParent<PlayerMovement>().Grounded == false)
            {
                if (collision.gameObject.tag == "Ground")
                {
                    this.gameObject.GetComponentInParent<PlayerMovement>().DoubleJumpCount = 0;
                    this.gameObject.GetComponentInParent<Rigidbody2D>().velocity = vector2;
                    this.gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = 0;
                    this.gameObject.GetComponentInParent<PlayerMovement>().Climbing = true;
                    this.gameObject.GetComponentInParent<PlayerMovement>().Grounded = false;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || (this.gameObject.GetComponentInParent<PlayerMovement>().Grounded == true))
        {
            this.gameObject.GetComponentInParent<PlayerMovement>().Climbing = false;

        }


    }


    private void OnTriggerExit2D(Collider2D collision)
    {

        this.gameObject.GetComponentInParent<PlayerMovement>().Climbing = false;

    }

}
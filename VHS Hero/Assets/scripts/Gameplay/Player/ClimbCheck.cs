using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbCheck : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 vector2 = new Vector2(0, 0);
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.GetComponentInParent<PlayerDataHolder>().ClimbWall == true && this.gameObject.GetComponentInParent<PlayerMovement>().Grounded == false) {
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.gameObject.GetComponentInParent<PlayerMovement>().Grounded == true)
        {
            this.gameObject.GetComponentInParent<PlayerMovement>().Climbing = false;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.GetComponentInParent<PlayerDataHolder>().ClimbWall == true)
        {
            if (collision.gameObject.tag == "Ground")
            {
                this.gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = 3;
                this.gameObject.GetComponentInParent<PlayerMovement>().Climbing = false;
            }
        }
    }
}
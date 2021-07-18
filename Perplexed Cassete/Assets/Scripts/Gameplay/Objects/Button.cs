using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Door[] doorsToOpen;
    public bool DestroySelfOnCompletion = true;
    private bool isPressed = false;
    public Animator animator;

    public bool IsPressed { get => isPressed; set => isPressed = value; }
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();

    }
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && collision.relativeVelocity.y !=0)
        {
            IsPressed = true;
            animator.SetBool("pressed", true);
            this.gameObject.GetComponent<Collider2D>().isTrigger = true;

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

    

   


    public void ButtonReset()
    {

        IsPressed = true;
        animator.SetBool("pressed", false);
        this.gameObject.GetComponent<Collider2D>().isTrigger = false;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //Variable List.//
    public float walkingSpeed;
    public float moveX;
    public float moveY;
    private Vector2 direction;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        GetInput();
        Move();
        //Input for the Animator.//
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        //Output for the Animator.//
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
    }

    void Move ()
    {
        transform.Translate(direction * walkingSpeed * Time.deltaTime); //DeltaTime makes moving more fluid.//
      //  print("Character is going: Horizontal: " + direction.x + " Vertical: " +  direction.y);
    }

    private void GetInput()
    { 
        direction = Vector2.zero; //RaZ.//
        //animator.SetLayerWeight(0, 1);
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);


        //Could be changed for InputManager's - Marc's way.//
        if (Input.GetAxisRaw("Vertical") > 0.5)
        {
            direction += Vector2.up;
        }
        if (Input.GetAxisRaw("Horizontal") < -0.5)
        {
            direction += Vector2.left;
        }
        if (Input.GetAxisRaw("Vertical") < -0.5)
        {
            direction += Vector2.down;
        }
        if (Input.GetAxisRaw("Horizontal") > 0.5)
        {
            direction += Vector2.right;
        }
    }

}

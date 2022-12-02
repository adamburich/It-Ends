using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public Animator animator;

    public float speed = 30f;
    float horizontalMove = 0f;
    bool jump = false;
    bool menuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetKeyDown("escape"))
        {
            menuOpen = !menuOpen;
            if (menuOpen)
            {

            }
        }
    }

    void FixedUpdate()
    {
        if (GameEnd.isEnding())
        {
            controller.Move(0, false);
            animator.SetFloat("Speed", Mathf.Abs(25));
            animator.SetFloat("VerticalSpeed", controller.getVerticalSpeed());
            animator.SetBool("Grounded", controller.isGrounded());
            animator.SetBool("Wallsliding", controller.wallSliding());
            animator.SetBool("HasNoVerticalSpeed", controller.getVerticalSpeed() == 0);
        }
        else
        {
            //Debug.Log("HORIZONTAL MOVE: " + horizontalMove);
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            animator.SetFloat("VerticalSpeed", controller.getVerticalSpeed());
            animator.SetBool("Grounded", controller.isGrounded());
            animator.SetBool("Wallsliding", controller.wallSliding());
            animator.SetBool("HasNoVerticalSpeed", controller.getVerticalSpeed() == 0);
        }
    }
}

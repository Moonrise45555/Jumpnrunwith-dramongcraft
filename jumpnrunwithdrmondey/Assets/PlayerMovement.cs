using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    [Header("Layar Mask")]
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Jump")]
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

   


    //Gets Rigidbody component
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Moves player on x axis
    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }



    void Update()
    {


        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        //checks if anything is in overlapcirclecast
        //turn twords you go
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        //flips
       

        //fixed double jump bug
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        //lets player jump
        if (isGrounded == true && Input.GetKeyDown("space") && isJumping == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        //makes you jump higher when you hold down space
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {

            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;

            }


        }

    }

}

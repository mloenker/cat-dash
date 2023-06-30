using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 20.0f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float jumpBufferLength = 0.2f;  // Buffer time to allow the jump animation to play

    private float movement = 0f;
    private float groundCheckRadius = 0.2f;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private bool isJumping = false;
    private float jumpBufferCount;  // Counter to keep track of how long since the jump button was pressed

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigidBody.gravityScale = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        // Check whether player is grounded
        bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = Time.time + jumpBufferLength;  // Set the jump buffer count when the jump button is pressed
        }

        if (jumpBufferCount > Time.time && grounded)
        {
            isJumping = true;
            jumpBufferCount = 0;  // Reset the buffer count
            animator.SetBool("isJumping", true);
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (grounded && rigidBody.velocity.y <= 0)
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

        if (movement != 0 && !isJumping) // We also check for isJumping here
        {
            animator.SetBool("isWalking", true);

            // Flip the character's sprite based on movement direction
            if (movement > 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (isJumping)
        {
            if (rigidBody.velocity.y > 0)
            {
                // Rotate the player slightly while rising
                transform.rotation = Quaternion.Euler(0, 0, 20 * Mathf.Sign(transform.localScale.x));
            }
            else if (rigidBody.velocity.y < 0)
            {
                // Rotate the player slightly while falling
                transform.rotation = Quaternion.Euler(0, 0, -20 * Mathf.Sign(transform.localScale.x));
            }
        }
        else
        {
            // Reset rotation when not jumping
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private void FixedUpdate()
    {
        Vector3 velocity = new Vector3(movement * speed, rigidBody.velocity.y, 0);
        rigidBody.velocity = velocity;
    }
}

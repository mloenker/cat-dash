using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 20.0f;
    public float maxSpeed = 10.0f;
    public float acceleration = 5.0f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float jumpBufferLength = 0.2f;  // Buffer time to allow the jump animation to play

    private float movement = 0f;
    private float groundCheckRadius = 0.2f;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private BoxCollider2D collider;
    private bool isJumping = false;
    private float jumpBufferCount;  // Counter to keep track of how long since the jump button was pressed
    private bool grounded;
    private string currentPlatform;
    private GameObject currentPlatformObject;
    private bool inDashZone = false;
    private GameObject currentDashPoint;

    public Sprite[] jumpSprites;

    [SerializeField] private AudioSource snowJumpSound;
    [SerializeField] private AudioSource snowLandSound;
    [SerializeField] private AudioSource iceJumpSound;
    [SerializeField] private AudioSource iceLandSound;
    [SerializeField] private AudioSource shroomSound;
    [SerializeField] private AudioSource meowSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        rigidBody.gravityScale = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCount = Time.time + jumpBufferLength;  // Set the jump buffer count when the jump button is pressed
        }

        if (jumpBufferCount > Time.time && grounded && currentPlatform != "bouncy")
        {
            isJumping = true;
            jumpBufferCount = 0;  // Reset the buffer count
            animator.SetBool("isJumping", true);
            Vector3 velocity = new Vector3(rigidBody.velocity.x, 0, 0);
            rigidBody.velocity = velocity;
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Destroy(currentPlatformObject);

            if (currentPlatform == "default"){
                snowJumpSound.Play();
            }else if (currentPlatform == "ice"){
                iceJumpSound.Play();
            }

        }

        if (grounded && rigidBody.velocity.y <= 0)
        {
            animator.enabled = true;
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


        // ### JUMPING ###
        if (isJumping)
        {

            if (rigidBody.velocity.y != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 20 * Mathf.Sign(transform.localScale.x) * (rigidBody.velocity.y/jumpForce));

            }
            animator.enabled = false;
            if (rigidBody.velocity.y > 0)
            {
                // use Mathf.Min to ensure the index never exceeds the length of jumpSprites[]
                int spriteIndex = Mathf.Min((int)(rigidBody.velocity.y / 15f * 2), 3);
                GetComponent<SpriteRenderer>().sprite = jumpSprites[spriteIndex];
            }
            else if (rigidBody.velocity.y < 0)
            {
                int spriteIndex = 2+(int)(Mathf.Abs(rigidBody.velocity.y) / 15f * 2);
                GetComponent<SpriteRenderer>().sprite = jumpSprites[spriteIndex];
            }


        }
        else
        {
            // Reset rotation when not jumping
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Dash
        if (Input.GetButtonDown("Jump") && inDashZone){
            meowSound.Play();
            Vector3 velocity = new Vector3(rigidBody.velocity.x, 0, 0);
            rigidBody.velocity = velocity;
            rigidBody.AddForce(new Vector2((currentDashPoint.transform.position.x - transform.position.x)*2, jumpForce*1.5f), ForceMode2D.Impulse);
            Destroy(currentDashPoint);
        }



    }

    private void OnCollisionEnter2D(Collision2D collision){
        //Debug.Log(collision.gameObject.name);
        if(collision.gameObject.layer == 3){
            grounded = true;
            currentPlatformObject = collision.gameObject;
            if(collision.gameObject.name.Contains("Default")){
                snowLandSound.Play();
                currentPlatform = "default";
            }else if(collision.gameObject.name.Contains("Ice")){
                currentPlatform = "ice";
                iceLandSound.Play();
            }else if(collision.gameObject.name.Contains("Bouncy")){
                currentPlatform = "bouncy";
                shroomSound.Play();
            }
        }

        if(collision.gameObject.name.Contains("Bouncy")){
            Vector3 velocity = new Vector3(rigidBody.velocity.x, 0, 0);
            rigidBody.velocity = velocity;
            rigidBody.AddForce(new Vector2(0, jumpForce*1.8f), ForceMode2D.Impulse);
            //Vector3 velocity = new Vector3(rigidBody.velocity.x, Mathf.Min(rigidBody.velocity.y, jumpForce*1.8f), 0);
            //rigidBody.velocity = velocity;
            shroomSound.Play();
        }

    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 3){
            grounded = false;
            currentPlatform = "none";
        }
    }

    // Test if in range of dash point
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "DashPoint"){
            inDashZone = true;
            currentDashPoint = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "DashPoint"){
            inDashZone = false;
        }
    }



    private void FixedUpdate()
    {
        
        if( Input.GetAxis("Horizontal")!=0){
            // Acceleration
            if (Mathf.Abs(rigidBody.velocity.x + movement*acceleration)<maxSpeed)
            {
                if(grounded){
                    Vector3 velocity = new Vector3(rigidBody.velocity.x + movement*acceleration, rigidBody.velocity.y, 0);
                    rigidBody.velocity = velocity;
                }else{
                    Vector3 velocity = new Vector3(rigidBody.velocity.x + movement*acceleration/10, rigidBody.velocity.y, 0);
                    rigidBody.velocity = velocity;
                }
            }
            // Decceleration on different platforms ground.collider.gameObject.name.Contains("Default")
        }else if (grounded){
            if (currentPlatform == "default"){
                if(rigidBody.velocity.x>0){
                    Vector3 velocity = new Vector3(Mathf.Min(0, rigidBody.velocity.x-acceleration), rigidBody.velocity.y, 0);
                    rigidBody.velocity = velocity;
                }else if (rigidBody.velocity.x<0){
                    Vector3 velocity = new Vector3(Mathf.Max(0, rigidBody.velocity.x+acceleration), rigidBody.velocity.y, 0);
                    rigidBody.velocity = velocity;
                }

            }
        }
    }


}

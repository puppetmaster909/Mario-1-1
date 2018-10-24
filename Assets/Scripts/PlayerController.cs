using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Animator anim;
    private bool facingRight = true;
    private int coinCount;

    public float speed;
    public float jumpForce;
    public Text coinText;

    //ground check
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    //Audio Clips
    public AudioClip jumpSmallSound;
    public AudioClip jumpLargeSound;
    public AudioClip coinSound;
    public AudioClip deathSound;
    public AudioClip goombaDeathSound;
    public AudioClip endOfLevelSound;


    // private float jumpTimeCounter;
    //public float jumpTime;
    //private bool isJumping;

    //audio stuff




    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coinCount = 0;
        SetCountText();
    }

    private void Update(){
       
    }

    
    // Update is called once per frame
    void FixedUpdate () {

        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.x *= 0.75f;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
    
        //Fake friction
        if (isOnGround)
        {
            rb2d.velocity = easeVelocity;
        }

        // Jumping
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && isOnGround == true)
        {
            anim.SetBool("ground", false);
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        //Vector2 movement = new Vector2(moveHorizontal, 0);

        // rb2d.AddForce(movement * speed);

        rb2d.velocity = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        anim.SetBool("ground", isOnGround);

        //Debug.Log(isOnGround);



        //stuff I added to flip my character
        if(facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

        //Section for Animations
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetTrigger("jump");
            GetComponent<AudioSource>().PlayOneShot(jumpSmallSound);
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    //Pickup Method
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            GetComponent<AudioSource>().PlayOneShot(coinSound);
            other.gameObject.SetActive(false);
            coinCount++;
            SetCountText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {


            if (Input.GetKey(KeyCode.UpArrow))
            {
               //rb2d.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
               rb2d.velocity = Vector2.up * jumpForce;
                
                
                // Audio stuff
              
                
                
            }
        }
    }

    public void SetCountText()
    {
        coinText.text = "Coins: " + coinCount.ToString();
    }

    public void addCoin()
    {
        coinCount++;
    }

}

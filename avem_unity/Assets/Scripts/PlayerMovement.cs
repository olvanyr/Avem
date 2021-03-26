using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;



public class PlayerMovement : MonoBehaviour
{



    public PlayerInput playerInput;
    //ref to componente 
    public Rigidbody2D rb;

    //movement var
    private float horizontalMovement;
    private float verticalMovement;
    public float moveSpeed;
    public float jumpForce;
    public float accelerationTime;
    public bool isGrounded;
    public float maxRbSpeed;
        //vector to apply the velocity 
        private Vector3 velocity = Vector3.zero;

    //animation var
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    //take reference for the empty gameobject groundcheck
    public Transform groundCheck;

    //public float groundCheckRadius;
    public Vector2 groundCheckSize;
    public LayerMask collisionLayer;

    // hovering var
    public float newGravityScale;
    public float gravityScale;

    //Scene transition coordinate
    public VectorValue startingPosition;

    //state var
    public string state;


    //track if the bird have something in his bick
    public bool haveObject = false;
    //sound

    private float soundTimer = 10;
    public AudioClip[] walkSound;
    public AudioClip[] landSound;

    public static PlayerMovement instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de PlayerMovement dans la scéne");
            return;
        }

        instance = this;


        playerInput = new PlayerInput();

        playerInput.NormalMovement.Jump.performed += context => Jump(); //second methode
        playerInput.NormalMovement.Action1.performed += context => Press(); //second methode
        playerInput.NormalMovement.Action2.performed += context => PickUp(); //second methode
        playerInput.NormalMovement.Move.performed += context => Movement(context.ReadValue<Vector2>()); //second methode
    }




    /*void OnGUI()
    {
        //This displays a Button on the screen at position (20,30), width 150 and height 50. The button’s text reads the last parameter. Press this for the SceneManager to load the Scene.
        if (GUI.Button(new Rect(20, 30, 150, 30), "Other Scene Single"))
        {
            //The SceneManager loads your new Scene as a single Scene (not overlapping). This is Single mode.
            SceneManager.LoadScene("SceneTest", LoadSceneMode.Single);
        }

        //Whereas pressing this Button loads the Additive Scene.
        if (GUI.Button(new Rect(20, 60, 150, 30), "Other Scene Additive"))
        {
            //SceneManager loads your new Scene as an extra Scene (overlapping the other). This is Additive mode.
            SceneManager.LoadScene("SceneTest", LoadSceneMode.Additive);
        }
    }*/

private void Start()
    {
        //!!!!!!!!!!!!!!!! uncomment this to make the player tp on start position
        //transform.position = startingPosition.initialValue;

        if (state == "move")//use to give instante control of the bird if she is not sleeping
        {
            animator.Play("idle");
        }
    }


    private void OnEnable() //if the script is enable, the we enable the PlayerInput we need to enable it
    {
        playerInput.Enable();
    }

    private void OnDisable() //if the script is disable, the we disable the PlayerInput
    {
        playerInput.Disable();
    }


    private void FixedUpdate()
    {
        
        if (state == "move")
        {

            //Is grounded ? 
            isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, collisionLayer);

            if (isGrounded)
            {
                if (soundTimer <= 5){soundTimer++;}

                    if (soundTimer == 5)
                    {
                        soundTimer = 10;
                        var sound = landSound[UnityEngine.Random.Range(0, landSound.Length - 1)];
                        AudioManager.instance.PlayClipAt(sound, "Sound", transform.position);
                        CameraFollow.instance.StartScreenShake(0.05f, 0.04f, 0f);
                    }

                rb.gravityScale = gravityScale;
                animator.SetBool("hover", false);
            }
            else
            {
                soundTimer = 0f;
            }
            
            MovePlayer(horizontalMovement, verticalMovement);

            

            
        }
        else
        {
            MovePlayer(0, 0);
        }

        //eat state 

        if (state == "eat")
        {

        }

        // Animation
        float charachterVelocity = math.abs((rb.velocity.x));
        float charachterVerticalVelocity = (rb.velocity.y);
        animator.SetFloat("speed", charachterVelocity);
        animator.SetFloat("verticalSpeed", charachterVerticalVelocity);
        animator.SetBool("isGrounded", isGrounded);

        if (state == "die")
        {
            animator.Play("Die");
        }
    }


    void Movement(Vector2 direction)
    {
        //Debug.Log("Player is moving : " + direction);

        horizontalMovement = direction.x * moveSpeed * Time.fixedDeltaTime;
        verticalMovement = direction.y * 0 * Time.fixedDeltaTime; //actualy, the vertical movvement is equal to zero
    }

    public void MovePlayer(float _horizontalMovement, float verticalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);//the force we aplie on x and the current y
        //rb.velocity = targetVelocity;
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, accelerationTime);
        flip(rb.velocity.x);
    }

    void Jump()
    {
        if (state == "move")
        {
            if (isGrounded && rb.velocity.magnitude < maxRbSpeed)
            {
                rb.AddForce(new Vector2(0, jumpForce));
            }
            else
            {

                if (rb.velocity.y < 0)
                {
                    animator.SetBool("hover",true);

                    rb.gravityScale = newGravityScale;
                }

            }
        }
    }

    void Press()
    {
        if (state == "move")
        {
            if (isGrounded)
            {
                animator.SetTrigger("isPressing");
                state = "press";
            }
        }

        haveObject = false;
    }

    void PickUp()
    {
        
        if (state == "pick")
        {
            state = "eat";
        }

        if (state == "move")
        {
            if (isGrounded && !haveObject)
            {
                animator.SetTrigger("isPickUping");
                state = "pick";
            }
            haveObject = false;
        }
    }

    void Move()
    {
        state = "move";
    }


    //-----------Animation
    void flip(float _velocity)
    {
        if (_velocity > 0.3f)
        {
            //transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.3f)
        {
            spriteRenderer.flipX = true;
            //transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    void WalkSound()
    {
        
        var sound = walkSound[UnityEngine.Random.Range(0, walkSound.Length-1)];
        
        var audioSource = AudioManager.instance.PlayClipAt(sound, "Sound", transform.position);
    }


    //--------------------------------- only debug
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}

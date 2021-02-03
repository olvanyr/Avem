using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

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

    //walljump var
    public bool isTouchingFront;
    public Transform frontCheck1;
    public Transform frontCheck2;
    private bool isWallSliging;
    public float wallSlidingSpeed;
    public float frontCheckRadius;

    // hovering var
    public float newGravityScale;
    public float gravityScale;

    //Scene transition coordinate
    public VectorValue startingPosition;

    //state var
    public string state;


    //track if the bird have something in his bick
    public bool haveObject = false;


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


    private void Start()
    {
        transform.position = startingPosition.initialValue;
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
                rb.gravityScale = gravityScale;
            }



            //Wall Slide
            if (Physics2D.OverlapCircle(frontCheck1.position, frontCheckRadius, collisionLayer) || Physics2D.OverlapCircle(frontCheck2.position, frontCheckRadius, collisionLayer))
            {
                isTouchingFront = true;
            }
            else
            {
                isTouchingFront = false;
            }

            if (isTouchingFront == true && isGrounded == false && Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isWallSliging = true;
            }
            else
            {
                isWallSliging = false;
            }

            if (isWallSliging)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
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
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0, jumpForce));
            }
            else
            {

                if (rb.velocity.y < 0)
                {
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

    void StopPickUp()
    {
        state = "move";
    }

    void StopPress()
    {
        state = "move";
    }


    //-----------Animation
    void flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            //transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
            //transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }


    //--------------------------------- only debug
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(frontCheck1.position, frontCheckRadius);
        Gizmos.DrawWireSphere(frontCheck2.position, frontCheckRadius);
        //Gizmos.DrawWireCube(frontCheck.position, wallCheckSize);
    }
}

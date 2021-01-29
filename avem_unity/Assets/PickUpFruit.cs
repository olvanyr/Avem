using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;


public class PickUpFruit : MonoBehaviour
{
    public GameObject self;
    public GameObject player;


    public GameObject popUp;
    public PopUp popUpScript;
    public string popUpText;
    public Sprite popUpSprite;

    public bool isPicked;
    public bool inRange;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public Rigidbody2D rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && PlayerMovement.instance.isPickUping)
        {
            isPicked = true;
            PlayerMovement.instance.haveObject = true;
            animator.Play("Picked");
        }



        if (isPicked)
        {
            flip();
            rb.simulated = false;
       
            self.transform.position = player.transform.position;
            float charachterVelocity = math.abs(PlayerMovement.instance.rb.velocity.x);
            animator.SetFloat("speed", charachterVelocity);

            if (math.abs(PlayerMovement.instance.rb.velocity.y) > 0.2)
            {
                isPicked = false;
                PlayerMovement.instance.haveObject = false;
                rb.simulated = true;
                animator.Play("OnGround");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            popUp.SetActive(true);
            popUpScript.UpdateHolder(popUpText, popUpSprite);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            popUp.SetActive(false);
            //popUp.UpdateHolder(popUpText, popUpSprite);
        }
    }

    void flip()
    {
        if (PlayerMovement.instance.spriteRenderer.flipX)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

        }
        else
        {

            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
}

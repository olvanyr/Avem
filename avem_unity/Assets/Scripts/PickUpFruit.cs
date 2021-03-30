using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;


public class PickUpFruit : MonoBehaviour
{
    public GameObject self;
    public GameObject player;

/*
    public GameObject popUp;
    public PopUp popUpScript;
    public string popUpText;
    public Sprite popUpSprite;*/

    public bool isPicked;
    public bool inRange;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    
    public Rigidbody2D rb;
    void Start()
    {
        player = PlayerMovement.instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        //Pick up fruit
        if (inRange && PlayerMovement.instance.state == "pick" && PlayerMovement.instance.haveObject == false)
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
            self.transform.eulerAngles = new Vector3(0, 0, 0);
            float charachterVelocity = math.abs(PlayerMovement.instance.rb.velocity.x);
            animator.SetFloat("speed", charachterVelocity);

            if ((PlayerMovement.instance.rb.velocity.y) > 0.2 && PlayerMovement.instance.isGrounded == false|| PlayerMovement.instance.haveObject == false && isPicked)
            {
                isPicked = false;
                PlayerMovement.instance.haveObject = false;
                rb.simulated = true;
                animator.Play("OnGround");
            }
        }

    }


    public void Eating()
    {
        if (PlayerMovement.instance.state == "eat")
        {
            PlayerHealth.instance.AddHealth(1);
            PlayerMovement.instance.haveObject = false;
            Destroy(self);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
            //popUp.SetActive(true);
            //popUpScript.UpdateHolder(popUpText, popUpSprite);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
            //popUp.SetActive(false);
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

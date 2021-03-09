﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public bool isInRange;
    public bool isMoving;
    public string direction;
    public float elevatorSpeed;


    public GameObject stopPointTop;
    public GameObject stopPointBottom;
    //public Transform transform;

    public BoxCollider2D boxCollider;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.gameObject.transform.parent = null;
            isInRange = false;
        }
    }

    private void Update()
    {
        if (isInRange && !isMoving)
        {
            PlayerMovement.instance.state = "elevator";
            isMoving = true;
            isInRange = false;

            if (direction == "up")
            {
                direction = "down";
            }
            else
            {
                direction = "up";
            }         
        }

        if (isMoving)
        {
            if (direction == "up")
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + elevatorSpeed, transform.position.z);
                if (transform.position.y >= stopPointTop.transform.position.y)
                {
                    transform.position = new Vector3(transform.position.x, stopPointTop.transform.position.y, transform.position.z);
                    isMoving = false;
                    PlayerMovement.instance.state = "move";
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - elevatorSpeed, transform.position.z);
                if (transform.position.y <= stopPointBottom.transform.position.y)
                {
                    transform.position = new Vector3(transform.position.x, stopPointBottom.transform.position.y, transform.position.z);
                    isMoving = false;
                    PlayerMovement.instance.state = "move";
                }
            }
        }
    }



    /*public bool isInRange;

    //public Transform transform;

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
            isInRange = true;
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.gameObject.transform.parent = null;
            isInRange = false;
            boxCollider.enabled = false;
        }
    }*/
}

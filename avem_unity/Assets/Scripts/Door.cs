using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Collider2D doorCollider;

    public Animator animator;

    public bool isDoorOpen = false;

    private bool isInRange = false;

    public int id;

    public bool isAttachedToAButton;
    public bool isAttachedToAScanner;

    public Button button;
    public Scanner scanner;



    private void Awake()
    {
        if (button != null)
        {
            isAttachedToAButton = true;
        }
        if (scanner != null)
        {
            isAttachedToAScanner = true;
        }
    }

    void Update()
    {
        if (isAttachedToAButton)
        {
            if (button.isOn)
            {
                isDoorOpen = true;
                
            }
            if(!button.isOn)
            {
                isDoorOpen = false;
            }
        }else if (isAttachedToAScanner)
        {
            if (scanner.isActivate)
            {
                isDoorOpen = true;

            }
            if (!scanner.isActivate)
            {
                isDoorOpen = false;
            }
        }
        else
        {
            if (isInRange)
            {
                isDoorOpen = true;
            }
            
            if(!isInRange)
            {
                isDoorOpen = false;
                
            }
        }

        if (isDoorOpen)
        {
            animator.SetBool("isDoorOpen", true);
        }
        else {
            animator.SetBool("isDoorOpen", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    void OpenDoor()
    {
        doorCollider.enabled = false;
    }
    void CloseDoor()
    {
        doorCollider.enabled = true;
    }
}

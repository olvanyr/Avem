using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Collider2D doorCollider;

    public Animator animator;

    public bool isDoorOpen;

    private bool isInRange = false;

    public bool openDoor;

    public int id;

    public bool isAttachedToAButton;
    public bool isAttachedToAScanner;

    public Button button;
    public Scanner scanner;



    private void Awake()
    {
        if (openDoor)
        {
            isDoorOpen = true;
        }
        else
        {
            isDoorOpen = false;
        }

        if (button != null)
        {
            isAttachedToAButton = true;
        }
        if (scanner != null)
        {
            isAttachedToAScanner = true;
        }

        if (isAttachedToAButton || isAttachedToAScanner)
        {
            var selfCollider = GetComponent<BoxCollider2D>();
            selfCollider.enabled = false;
        }
    }

    void Update()
    {
        if (isAttachedToAScanner && isAttachedToAButton)
        {

            if (button.isOn || scanner.isActivate)
            {
                if (openDoor)
                {
                    isDoorOpen = true;
                }
                else
                {
                    isDoorOpen = false;
                }

            }
            if (!button.isOn && !scanner.isActivate)
            {
                if (openDoor)
                {
                    isDoorOpen = false;
                }
                else
                {
                    isDoorOpen = true;
                }
            }
        }
        else if (isAttachedToAButton)
        {
            if (button.isOn)
            {
                if (openDoor)
                {
                    isDoorOpen = true;
                }
                else
                {
                    isDoorOpen = false;
                }
            }
            if(!button.isOn)
            {
                if (openDoor)
                {
                    isDoorOpen = false;
                }
                else
                {
                    isDoorOpen = true;
                }
            }
        }else if (isAttachedToAScanner)
        {
            if (scanner.isActivate)
            {
                if (openDoor)
                {
                    isDoorOpen = true;
                }
                else
                {
                    isDoorOpen = false;
                }

            }
            if (!scanner.isActivate)
            {
                if (openDoor)
                {
                    isDoorOpen = false;
                }
                else
                {
                    isDoorOpen = true;
                }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{

    public Collider2D doorCollider;

    public Animator animator;

    public bool isDoorOpen;
    
    private bool isInRange = false;

    // Start is called before the first frame update

    private void Start()
    {

        //DoorAlreadyOpen();
        if(isDoorOpen)
        {
            DoorAlreadyOpen();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            animator.SetTrigger("openDoor");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }


    void OpenDoor() 
    {   
        doorCollider.enabled = false;
        isDoorOpen = true;
    }

    void DoorAlreadyOpen()
    {
        doorCollider.enabled = false;
        animator.SetTrigger("DoorAlreadyOpen");
    }
}

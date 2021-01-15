using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : MonoBehaviour
{

    public Collider2D doorCollider;

    public Animator animator;

    public bool isDoorOpen = false;
    
    private bool isInRange = false;

    public int id;

   

    // Start is called before the first frame update

    private void Start()
    {

        if (GameSaveManager.instance.doorData.Contains(id))
        {
            isDoorOpen = true;
        }


        //DoorAlreadyOpen();
        if (isDoorOpen)
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

        if (GameSaveManager.instance.doorData.Contains(id))
        {
            Debug.LogError("the current door id is already atributed");
        }
        GameSaveManager.instance.doorData.Add(id);
    }

    void DoorAlreadyOpen()
    {
        doorCollider.enabled = false;
        animator.SetTrigger("DoorAlreadyOpen");
    }
}

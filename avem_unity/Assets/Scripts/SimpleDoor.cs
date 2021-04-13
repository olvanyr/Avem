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

    private AudioClip hitSound;

    public bool isAttachedToAButton;
    public bool isAttachedToAScanner;

    public Button button;
    public Scanner scanner;

    //test
    public List<string> doorData = new List<string>();

    public string doorId;

    public GlobalVariables globalVar;


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

        hitSound = globalVar.doorHit1;

        if (isAttachedToAButton ||isAttachedToAScanner)
        {
            var selfCollider = GetComponent<BoxCollider2D>();
            selfCollider.enabled = false;
        }
    }

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
        if (isAttachedToAButton)
        {
            if (button.isOn)
            {
                animator.SetTrigger("openDoor");
            }
        }else if (isAttachedToAScanner)
        {
            if (scanner.isActivate)
            {
                animator.SetTrigger("openDoor");
            }
        }
        else
        {
            if (isInRange)
            {
                animator.SetTrigger("openDoor");
            }
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
        AudioManager.instance.PlayClipAt(hitSound, "Sound", transform.position);

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

using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class OpenTimerDoor : MonoBehaviour
{
    public Collider2D doorCollider;

    public Animator animator;

    public bool isDoorOpen = false;

    public int id;

    private AudioClip hitSound;

    public float timeSpawn;

    public TextMeshPro text;

    public Light2D doorLight;


    public Color color1;
    public Color color2;

    public GlobalVariables globalVar;

    private void Awake()
    {
        hitSound = globalVar.doorHit1;
    }

    private void Start()
    {

        doorCollider.enabled = true;
        SetColor(color2);

        if (GameSaveManager.instance.doorData.Contains(id))
        {
            isDoorOpen = true;
        }


        if (isDoorOpen || timeSpawn < Timer.instance.currentTime)
        {
            DoorAlreadyOpen();
        }



    }


    void Update()
    {

        float currentTime = Timer.instance.currentTime;

        float time = (timeSpawn - currentTime);

        if (time < -999)
        {
            time = -999;
        }


        text.text = time.ToString("F2");

        if (timeSpawn < Timer.instance.currentTime)
        {
            animator.SetTrigger("openDoor");
            SetColor(color1);
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
        SetColor(color1);
        doorCollider.enabled = false;
        animator.SetTrigger("DoorAlreadyOpen");
    }

    void SetColor(Color c)
    {
        doorLight.color = c;
        text.color = c;
    }
}

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

    public float timeSpawn;

    public TextMeshPro text;

    public Light2D doorLight;


    public Color color1;
    public Color color2;

    private void Start()
    {

        doorCollider.enabled = false;
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
        doorCollider.enabled = true;
        isDoorOpen = true;

        

        if (GameSaveManager.instance.doorData.Contains(id))
        {
            Debug.LogError("the current door id is already atributed");
        }
        GameSaveManager.instance.doorData.Add(id);
    }

    void DoorAlreadyOpen()
    {
        SetColor(color1);
        doorCollider.enabled = true;
        animator.SetTrigger("DoorAlreadyOpen");
    }

    void SetColor(Color c)
    {
        doorLight.color = c;
        text.color = c;
    }
}

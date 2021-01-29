using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class TimerDoor : MonoBehaviour
{
    public Collider2D doorCollider;

    public Animator animator;

    public bool isDoorClose = false;

    public int id;

    public float timeSpawn;

    public TextMeshPro text;

    public Light2D doorLight;


    public Color color1;
    public Color color2;

    private void Start()
    {

        doorCollider.enabled = false;
        SetColor(color1);

        if (GameSaveManager.instance.doorData.Contains(id))
        {
            isDoorClose = true;
        }


        if (isDoorClose || timeSpawn < Timer.instance.currentTime)
        {
            DoorAlreadyClosed();
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
            animator.SetTrigger("CloseDoor");
        }
    }


    void CloseDoor()
    {
        doorCollider.enabled = true;
        isDoorClose = true;

        SetColor(color2);

        if (GameSaveManager.instance.doorData.Contains(id))
        {
            Debug.LogError("the current door id is already atributed");
        }
        GameSaveManager.instance.doorData.Add(id);
    }

    void DoorAlreadyClosed()
    {
        SetColor(color2);
        doorCollider.enabled = true;
        animator.SetTrigger("DoorAlreadyClosed");
    }

    void SetColor(Color c)
    {
        doorLight.color = c;
        text.color = c;
    }
}

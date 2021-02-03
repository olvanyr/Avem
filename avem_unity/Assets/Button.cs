using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class Button : MonoBehaviour
{

    public bool isOn;

    public bool inRange;

    public SpriteRenderer[] cablesSprite;

    public Color colorOn;
    public Color colorOff;

    public float timer;
    public float waitTime;

    public TextMeshPro text;
    public Light2D buttonLight;

    public Color lightColor1;
    public Color lightColor2;

    public Animator animator;
    public PlayerMovement player;

    public bool isHorizontal;
    // Start is called before the first frame update
    void Start()
    {
        //init cable color
        for (int i = 0; i < cablesSprite.Length; i++)
        {
            cablesSprite[i].color = colorOff;
        }

        //init light and text color
        if (waitTime == 0)
        {
            text.enabled = false;
            buttonLight.enabled = false;
        }
        else
        {
            SetColor(lightColor1);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (inRange && isHorizontal && !isOn)
        {
            ButtonOn();
        }


        //pushing the button to open door
        if (inRange && PlayerMovement.instance.state == "press" && !isOn)
        {
            ButtonOn();
        }

        //reset the button after a certain time
        float deltaTime = Time.deltaTime;

        
        timer = (timer - deltaTime);

        if (inRange && isHorizontal)
        {
            timer = waitTime;
        }

            if (timer < 0)
        {
            timer = 0;
        }


        text.text = timer.ToString("F2");

        if (timer == 0 && isOn && waitTime != 0)
        {
            ButtonOff();
        }
    }

    public void ButtonOn()
    {
        isOn = true;
        animator.SetBool("IsOn", true);
        timer = waitTime;
        SetColor(lightColor2);
    }


    public void ButtonOff()
    {
        animator.SetBool("IsOff", true);
        isOn = false;
        SetColor(lightColor1);
    }

    public void CablesColorOn()
    {
        for (int i = 0; i < cablesSprite.Length; i++)
        {
            cablesSprite[i].color = colorOn;
        }
        animator.SetBool("IsOn", false);
    }

    public void CablesColorOff()
    {
        for (int i = 0; i < cablesSprite.Length; i++)
        {
            cablesSprite[i].color = colorOff;
        }
        animator.SetBool("IsOff", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || isHorizontal && collision.CompareTag("PhysicObject"))
        {
            inRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || isHorizontal && collision.CompareTag("PhysicObject"))
        {
            inRange = false;
        }
    }


    void SetColor(Color c)
    {
        buttonLight.color = c;
        text.color = c;
    }
}

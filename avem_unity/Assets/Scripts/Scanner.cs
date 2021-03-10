using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Scanner : MonoBehaviour
{
    public bool isActivate;
    public bool inRange;

    public bool red;
    public bool blue;
    public bool green;

    public Color colorRed;
    public Color colorBlue;
    public Color colorGreen;

    public Animator animator;

    public Light2D scannerLight;

    private void Start()
    {
        animator.SetBool("Red", red);
        animator.SetBool("Blue", blue);
        animator.SetBool("Green", green);
        if (red){scannerLight.color = colorRed;}
        if (blue){scannerLight.color = colorBlue;}
        if (green){scannerLight.color = colorGreen;}
    }

    void Update()
    {
        if (isActivate)
        {
            scannerLight.intensity = 1.2f;
        }
        else {
            scannerLight.intensity = 0.5f;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        inRange = true;

        if (collision.GetComponent<Red>() && red || collision.GetComponent<Blue>() && blue || collision.GetComponent<Green>() && green)
        {
            StartScanning();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        if (collision.GetComponent<Red>() && red || collision.GetComponent<Blue>() && blue || collision.GetComponent<Green>() && green)
        {
            StopScanning();
        }
    }

    public void StartScanning()
    {
        if (isActivate == false)
        {
            animator.SetBool("Activate", true);
        }
        isActivate = true;
    }
    public void StopScanning()
    {
        isActivate = false;
        animator.SetBool("Activate", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Scanner : MonoBehaviour
{
    public bool isActivate;
    public bool inRange;

    public Animator animator;

    public Light2D scannerLight;

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
        if (collision.GetComponent<Red>())
        {

            if (isActivate == false)
            {
                animator.SetBool("Activate", true);
            }
            isActivate = true;    
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        if (collision.GetComponent<Red>())
        {
            isActivate = false;
            animator.SetBool("Activate", false);
        }
    }
}

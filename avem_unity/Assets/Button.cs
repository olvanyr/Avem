using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public bool isOn;

    public bool inRange;

    public SpriteRenderer[] cablesSprite;

    public Color color;

    public Animator animator;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && PlayerMovement.instance.isPressing)
        {
            isOn = true;
            animator.SetTrigger("isOn");
        }
    }



    public void UpdateCablesColor()
    {
        for (int i = 0; i < cablesSprite.Length; i++)
        {
            cablesSprite[i].color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}

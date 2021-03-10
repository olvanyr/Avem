using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{

    public int damage;

    private bool inRange;

    public PlayerHealth playerHealth;

    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;

    private void Awake()
    {
        SetHitboxSize();
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

    private void Update()
    {
        if (inRange)
        {
            playerHealth.ReduceHealth(damage);
        }   
    }

    public void SetHitboxSize()
    {
        boxCollider.size = new Vector2(spriteRenderer.size.x - 1.8f, boxCollider.size.y);
    }
}

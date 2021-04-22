using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropingFruit : MonoBehaviour
{
    public bool droped;
    public Rigidbody2D rb;

    private void Awake()
    {

        rb.simulated = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("player enter trigger");
                rb.simulated = true;
                Destroy(gameObject);

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public bool isInRange;

    //public Transform transform;

    public BoxCollider2D boxCollider;
    public Rigidbody2D rb;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
            isInRange = true;
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            collision.gameObject.transform.parent = null;
            isInRange = false;
            boxCollider.enabled = false;
        }
    }
}

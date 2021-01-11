using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSnapToRoom : MonoBehaviour
{

    public Vector2 cameraChange;
    public Vector3 playerChange;

    private CameraFollow camScript;

    // Start is called before the first frame update
    void Start()
    {
        camScript = Camera.main.GetComponent<CameraFollow>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            camScript.minPosition += cameraChange;
            camScript.maxPosition += cameraChange;
            collision.transform.position += playerChange;
        }
    }
}

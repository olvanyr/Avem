using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{

    public BoxCollider2D boxCollider;
    public CameraFollow cameraFollow;

    public float boxX;
    public float boxY;
    public float halfHeight;
    public float halfWidth;

    public Vector2 minPosition;
    public Vector2 maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        boxX = boxCollider.size.x;
        boxY = boxCollider.size.y;

        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;

        minPosition.x = transform.position.x - ((boxX/2) - halfWidth);
        minPosition.y = transform.position.y - ((boxY/2) - halfHeight);
        maxPosition.x = transform.position.x + ((boxX/2) - halfWidth);
        maxPosition.y = transform.position.y + ((boxY/2) - halfHeight);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cameraFollow.minPosition = minPosition;
            cameraFollow.maxPosition = maxPosition;
        }
        
    }
}

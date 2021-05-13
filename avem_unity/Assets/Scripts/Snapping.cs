using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{

    public float cameraSize;
    //public Color color;

    public BoxCollider2D boxCollider;
    public CameraFollow cameraFollow;

    public float boxX;
    public float boxY;
    public float halfHeight;
    public float halfWidth;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    public GameObject[] ObjectManagment;
    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = CameraFollow.instance.GetComponent<CameraFollow>();

        boxX = boxCollider.size.x;
        boxY = boxCollider.size.y;

        Camera camera = Camera.main;
        halfHeight = cameraSize;
        halfWidth = camera.aspect * halfHeight;

        minPosition.x = transform.position.x - ((boxX/2) - halfWidth);
        minPosition.y = transform.position.y - ((boxY/2) - halfHeight);
        maxPosition.x = transform.position.x + ((boxX/2) - halfWidth);
        maxPosition.y = transform.position.y + ((boxY/2) - halfHeight);

        for (int i = 0; i < ObjectManagment.Length; i++)
        {
            ObjectManagment[i].SetActive(false);
        }
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            if (Camera.main.orthographicSize != cameraSize)
            {
                Camera.main.orthographicSize = cameraSize;
            }
            cameraFollow.minPosition = minPosition;
            cameraFollow.maxPosition = maxPosition;
            if (ObjectManagment.Length < 0)
            {
                for (int i = 0; i < ObjectManagment.Length; i++)
                {
                    ObjectManagment[i].SetActive(true);
                }
            }
            

            //CameraFollow.instance.CamerColorUpdate(color);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ObjectManagment.Length < 0)
        {
            for (int i = 0; i < ObjectManagment.Length; i++)
            {
                ObjectManagment[i].SetActive(false);
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //var size = new Vector3(boxCollider.size.x);
        Gizmos.DrawWireCube(transform.position, boxCollider.size);
    }
}

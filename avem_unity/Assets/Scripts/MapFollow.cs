using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFollow : MonoBehaviour
{

    public float xscale;
    public float yscale;
    public Vector2 origin;

    private Transform playerTransform;
    public Transform mapTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = PlayerMovement.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        var position = new Vector3(((playerTransform.position.x - origin.x) * xscale) + mapTransform.position.x, ((playerTransform.position.y - origin.y) * yscale) + mapTransform.position.y, transform.position.z);

        transform.position = position;
    }
}

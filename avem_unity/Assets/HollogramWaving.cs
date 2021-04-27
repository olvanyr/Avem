﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class HollogramWaving : MonoBehaviour
{

    public bool isWaving;

    public float rng;

    public float div;
    // Start is called before the first frame update
    void Start()
    {
        if (isWaving)
        {
            rng = UnityEngine.Random.Range(0f, 100f);
        }
        else
        {
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (math.sin(Time.time + rng) /div), transform.position.z);
    }
}

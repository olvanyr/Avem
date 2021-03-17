using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float startTime;
    public float currentTime;



    public static Timer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de Timer dans la scéne");
            Destroy(this);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }


    void Start()
    {
        startTime = Time.time;
        //Debug.Log("start time  : " + startTime.ToString());
    }
    void FixedUpdate()
    {
        //ellapsed = time.time - startTime;
        currentTime = Time.time-startTime;
        //Debug.Log("current time  : " + currentTime.ToString());
    }
}

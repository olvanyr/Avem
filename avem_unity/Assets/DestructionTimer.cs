using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;


public class DestructionTimer : MonoBehaviour
{

    public TextMeshPro text;

    public Light2D timerLight;

    public float runDuration;

    void Update()
    {
        float currentTime = Timer.instance.currentTime;

        float time = (runDuration - currentTime);

        if (time <= 0)
        {
            time = 0;
            Restart.instance.RestartGame();
        }

        text.text = time.ToString("F2");
    }
}

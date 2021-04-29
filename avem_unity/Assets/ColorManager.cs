using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;

public class ColorManager : MonoBehaviour
{
    public Color backWallColor;
    public Color WallColor;

    public Tilemap backWall;
    public Tilemap backWallProps;
    public Tilemap wall;
    public Tilemap wallDetail;
    public Tilemap backProps;
    public Light2D globalLight;

    private void Awake()
    {
        backWall.color = (backWallColor);
        backWallProps.color = (backWallColor);
        wall.color = (WallColor);
        wallDetail.color = (WallColor);
        backProps.color = (WallColor);
        globalLight.intensity = 0.5f;
    }
}

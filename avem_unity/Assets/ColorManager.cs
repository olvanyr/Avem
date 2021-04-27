using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ColorManager : MonoBehaviour
{
    public Color backWallColor;
    public Color WallColor;

    public Tilemap backWall;
    public Tilemap backWallProps;
    public Tilemap wall;
    public Tilemap wallDetail;
    public Tilemap backProps;

    private void Awake()
    {
        backWall.color = (backWallColor);
        backWallProps.color = (backWallColor);
        wall.color = (WallColor);
        wallDetail.color = (WallColor);
        backProps.color = (WallColor);

    }
}

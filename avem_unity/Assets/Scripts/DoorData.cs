using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorData
{
    public int id;
    public bool doorState;

    public DoorData(int _id, bool _isDoorOpen)
    {
        id = _id;
        doorState = _isDoorOpen;
    }
}

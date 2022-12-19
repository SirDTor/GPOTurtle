using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Box : MovingObject
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        //пассивным объектам нужно ставить Direction.NONE чтобы при инициализации они никуда не убегали
        currentDirection = Direction.NONE;
        collider.enabled = false;
    }

}

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
        //��������� �������� ����� ������� Direction.NONE ����� ��� ������������� ��� ������ �� �������
        currentDirection = Direction.NONE;
        collider.enabled = false;
    }

}

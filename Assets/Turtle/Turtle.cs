using UnityEngine;
using System.Collections;
using System;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Turtle : MovingObject
{
    //���������� ��������
    public int fuel; //������� ������� � ��.
    public int fuelPerStep; //����������� ������� �� ���� ���
    

    protected new void Start()
    {
        base.Start();
    }

    //��������� ���, ���� �������� ������ ����
    public void Step(int arg)
    {
        if (arg > 0)
        {
            if (!CheckFuel())
            {
                Debug.Log("��� ����������. ������� �����������");
                return;
            }
            if (busyFlag)
            {
                Debug.Log("���������� ��� ��� �� ��������");
                return;
            }
            if (InspectStep(currentDirection))
            {
                MoveObject(arg);
                fuel -= fuelPerStep;
            }
            
        }
        else MoveObject(-arg);
    }

    //���������� ����������� ����
    public bool CheckFuel()
    {
        return (fuel - fuelPerStep >= 0);
    }

    // Update is called once per frame
    //����� �������, ���� ������
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Step(1);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Turn(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Turn(1);
        }
    }

    new public void MoveObject(int arg)
    {
        base.MoveObject(1);
    }

    void CHANGEFUEL(int value)
    {
        fuel += value;
    }

    //���� ���������� ��������� ������
    void FINISH(int value)
    {
        fuel += 999;
        Debug.Log("Finish");

    }
}

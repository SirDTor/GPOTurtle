using UnityEngine;
using System.Collections;
using System;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class Turtle : MovingObject
{
    //переменные черепахи
    public int fuel; //уровень топлива в ед.
    public int fuelPerStep; //потребление топлива за один шаг
    

    protected new void Start()
    {
        base.Start();
    }

    //выполняет шаг, если аргумент больше нуля
    public void Step(int arg)
    {
        if (arg > 0)
        {
            if (!CheckFuel())
            {
                Debug.Log("Шаг невозможен. Топливо закончилось");
                return;
            }
            if (busyFlag)
            {
                Debug.Log("Предыдущий шаг еще не закончен");
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

    //возвращает возможность шага
    public bool CheckFuel()
    {
        return (fuel - fuelPerStep >= 0);
    }

    // Update is called once per frame
    //можно стереть, если мешает
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

    //сюда прикрутить обработку финиша
    void FINISH(int value)
    {
        fuel += 999;
        Debug.Log("Finish");

    }
}

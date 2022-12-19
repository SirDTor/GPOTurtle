using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingObject : ObjectOnMap
{
    //��������� �������� �����������. ������������ ��� ������� ������� �����������
    //� ��������
    public enum Direction
    {
        UP = 0,
        RIGHT = 1,
        DOWN = 2,
        LEFT = 3,
        NONE = 4,
    }

    //id � ��������� ����. id ���� ����� � ����� �������� 
    //movingObjects � mapManager'�. ���������� -1, ��� ��������
    //��������, ��� ������� ��� ��� � �����, � ������ ���������� ��
    public int idOnMap = -1; 
    //������� ����������� �������� �������
    public Direction currentDirection;
    //���� ���� ���������� true, ������� ������ ������������ ��� ��� �������
    //�. �. �� ��� ��������� ��� ��� �������
    public bool busyFlag = false; 
    //������ ����������� ��� ���������� ����
    public Vector3 delta;
    //�������� �����. ����� ��� ���������� ������� �� �����
    //� �������� �� �������������
    public MapManager mapManager;

    //��������� ����������� ����  � ������ ����������� (������ ����������� �����)
    public bool InspectStep(Direction direction)
    {
        Debug.Log(this.name + "�����");
        Vector3 dirDelta = SetVector(direction);
        return mapManager.Inspect(transform.position + dirDelta, direction);
    }

    //�������� ��������. �� �������
    private IEnumerator MoveCoroutine()
    {
        busyFlag = true;
        Vector3 startPosition = transform.position;
        Vector3 newPosition = new Vector3(transform.position.x + delta.x, transform.position.y + delta.y, transform.position.z + delta.z);
        for (float i = 0; i < 1; i += Time.deltaTime / stepTime)
        {
            transform.position = Vector3.Lerp(startPosition, newPosition, i);
            yield return null;
        }
        transform.position = newPosition;
        busyFlag = false;
    }

    //������� ������� ���������� ����
    public void MoveObject(int arg)
    {
        if (arg > 0)
        {
            if (InspectStep(currentDirection))
            {
                Debug.Log(this.name + "���");
                StartCoroutine(MoveCoroutine());
            }
            else Debug.Log(this.name + ": ���������� ��������� ���");
        }
    }

    //������� ��������. �� �������
    private void TurnRight()
    {
        if (!busyFlag)
        {
            if (currentDirection == Direction.LEFT)
                currentDirection = Direction.UP;
            else currentDirection++;
            delta = SetVector(currentDirection);
            StartCoroutine(RotateCoroutine());
        }
        else Debug.Log("���������� ������� ��� �� ��������");
    }
    private void TurnLeft()
    {
        if (!busyFlag)
        {
            if (currentDirection == Direction.UP)
                currentDirection = Direction.LEFT;
            else currentDirection--;
            delta = SetVector(currentDirection);
            StartCoroutine(RotateCoroutine());
        }
        else Debug.Log("���������� ������� ��� �� ��������");
    }

    //������� ������� ��������
    //�� ������� ��� ��������� >0 � ��������
    public void Turn(int arg)
    {
        if (arg == 0) return;
        if (arg > 0) TurnRight();
        if (arg < 0) TurnLeft();
    }

    //�������� ��������. �� �������
    private IEnumerator RotateCoroutine()
    {
        busyFlag = true;
        //collider.isTrigger = false;
        Quaternion startRotation = transform.rotation;
        for (float i = 0; i < 1; i += Time.deltaTime / stepTime)
        {
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0, 0, -(int)currentDirection * 90), i);
            yield return null;
        }
        transform.rotation = Quaternion.Euler(0, 0, -(int)currentDirection * 90);
        //collider.isTrigger = true;  
        busyFlag = false;
    }

    //��������� ������� �� enum Direction
    //��� ������ ��� ��������� ��������� ������ ��� �������� �����������
    public Vector3 SetVector(Direction dir = Direction.NONE)
    {
        Vector3 vec = new Vector3();
        switch (dir)
        {
            case Direction.UP:
                vec.Set(0, 1, 0);
                break;
            case Direction.RIGHT:
                vec.Set(1, 0, 0);
                break;
            case Direction.DOWN:
                vec.Set(0, -1, 0);
                break;
            case Direction.LEFT:
                vec.Set(-1, 0, 0);
                break;
            case Direction.NONE:
                delta = SetVector(currentDirection);
                break;
        }
        return vec;
    }


    protected new void Start()
    {
        base.Start();
        idOnMap = MapManager.SetObject(this, idOnMap);
        delta = SetVector(currentDirection);
        InspectStep(currentDirection);
    }



}

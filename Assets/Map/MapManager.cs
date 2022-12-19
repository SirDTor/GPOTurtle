using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap map;

    //������ ��������� �������� �� �����. ������� ����������� ����
    //��� �������������
    public static List<MovingObject> movingObjects = new List<MovingObject>(); 

    //���� ���������� ���������� ������ ��� �������� �� ������� �����������
    //���� �� ����������� ����������� ��������� ������, �� ��������� ���� �� �������
    //������������ ��������� ������� ����� � ������ �����������
    //�� ����� ������ z � ���������/��������
    //���� z ����� ���������� �� ����������� - ��� �� �����������, � ���/�������
    //!��������������, ��� �������� ����������� ��������������� ����� �������� ����!
    public bool Inspect(Vector3 point, MovingObject.Direction direction)
    {
        MovingObject obj = GetObject(point);
        if (obj ?? false)
        {
            if (obj.InspectStep(direction))
            {
                Debug.Log(this.name + "���");
                obj.currentDirection = direction;
                obj.SetVector();
                obj.MoveObject(1);
                return true;
            }
            Debug.Log(this.name + "����");
            return false;
        }
        Debug.Log(this.name + "�����");
        return !map.HasTile(Vector3Int.FloorToInt(point));
    }

    //���������� ������� � ������
    public static int SetObject(MovingObject obj, int id = -1)
    {
        if (id != -1) return obj.idOnMap;
        movingObjects.Add(obj);
        return movingObjects.Count - 1;
    }

    //�������� ������� ������� � ������ �����������
    //���������� ������ ��� null
    public static MovingObject GetObject(Vector3 point)
    {
        if (movingObjects.Count == 0) return null;
        for (int i=0; i<movingObjects.Count; i++)
        {
            if (movingObjects[i].transform.position == point)
            {
                Debug.Log(movingObjects[i].name);
                return movingObjects[i];
            }
        }
        return null;
    }

    //������ ������ ������� �� ��� id
    public static MovingObject GetObject(int idOnMap)
    {
        return movingObjects[idOnMap];
    }

    


}

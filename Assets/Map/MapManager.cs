using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public Tilemap map;

    //список подвижных объектов на карте. объекты добавл€ютс€ сами
    //при инициализации
    public static List<MovingObject> movingObjects = new List<MovingObject>(); 

    //сюда передаютс€ координаты €чейки дл€ проверки на наличие преп€тстви€
    //если по координатам встречаетс€ подвижный объект, он провер€ет себ€ по цепочке
    //преп€тствием считаетс€ наличие тайла в данных координатах
    //на одном уровне z с черепахой/объектом
    //если z тайла отличаетс€ от черепашьего - это не преп€тствие, а пол/потолок
    //!предполагаетс€, что проверка совершаетс€ непосредственно перед попыткой шага!
    public bool Inspect(Vector3 point, MovingObject.Direction direction)
    {
        MovingObject obj = GetObject(point);
        if (obj ?? false)
        {
            if (obj.InspectStep(direction))
            {
                Debug.Log(this.name + "тут");
                obj.currentDirection = direction;
                obj.SetVector();
                obj.MoveObject(1);
                return true;
            }
            Debug.Log(this.name + "аааа");
            return false;
        }
        Debug.Log(this.name + "тесто");
        return !map.HasTile(Vector3Int.FloorToInt(point));
    }

    //добавление объекта в список
    public static int SetObject(MovingObject obj, int id = -1)
    {
        if (id != -1) return obj.idOnMap;
        movingObjects.Add(obj);
        return movingObjects.Count - 1;
    }

    //проверка наличи€ объекта в данных координатах
    //возвращает объект или null
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

    //просто выдача объекта по его id
    public static MovingObject GetObject(int idOnMap)
    {
        return movingObjects[idOnMap];
    }

    


}

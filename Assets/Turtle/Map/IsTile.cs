using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//класс провер€ет тайл по координатам
//дл€ каждого типа тайлов создаетс€ свой экземпл€р класса
public class IsTile : MonoBehaviour
{
    private Tilemap map;
    public TileBase sample;
    public int z;

    private void Awake()
    {
        map = GetComponent<Tilemap>();
    }

    public bool Inspect(Vector2 tile)
    {
        
        if (map.GetTile(new Vector3Int((int)tile.x, (int)tile.y, z)).Equals(sample))
            return false;
        else return true;
    }

}

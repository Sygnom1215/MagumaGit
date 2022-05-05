using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BurnTile : MonoBehaviour
{
    public Tilemap tilemap;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void Burn(Vector3 pos)
    {
        Vector3Int cellPos = tilemap.WorldToCell(pos);

        tilemap.SetTile(cellPos, null);
    }
}

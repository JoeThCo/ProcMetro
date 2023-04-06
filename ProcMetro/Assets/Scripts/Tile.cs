using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Tile
{
    public string id;
    public TileBase tileBase;
    public TileType tileType;

    public Vector3Int Position { get; private set; }
    public Tile Previous { get; set; }
    public float Cost { get; set; }

    public Tile(int x, int y, string id)
    {
        Tile t = Room.GetTile(id);

        this.id = id;
        this.tileBase = t.tileBase;
        this.tileType = t.tileType;
        this.Position = new Vector3Int(x, y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu()]
public class TileSet : ScriptableObject
{
    [System.Serializable]
    public struct Tile 
    {
        public string name;
        public TileBase tileBase;
    }

    public Tile[] allTiles;
}

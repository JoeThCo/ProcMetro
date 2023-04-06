using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;

    [Space(10)]

    Grid2D<Tile> grid;

    Vector2Int roomSize;
    Direction[] connections;

    static TileSet tileSetStatic;

    RoomPathFinding pathFinding;

    #region Init
    public void RoomInit(TileSet tileSet)
    {
        tileSetStatic = tileSet;

        //numbers init
        roomSize = new Vector2Int(Random.Range(5, 20), Random.Range(5, 20));
        connections = MakeDirections();
        grid = new Grid2D<Tile>(roomSize, Vector3Int.zero);

        for (int y = 0; y < roomSize.y; y++)
        {
            for (int x = 0; x < roomSize.x; x++)
            {
                grid[x, y] = new Tile(x, y, "Wall");
            }
        }

        Debug.Log(grid[0, 0].id);

        pathFinding = new RoomPathFinding(grid);

        List<Vector3Int> testPath = pathFinding.GetPath(Vector3Int.zero, Vector3Int.one * 4);

        PrintInfo();
    }

    Direction[] MakeDirections()
    {
        List<Direction> output = new List<Direction>();
        int count = Random.Range(1, 5);

        for (int i = 0; i < 25; i++)
        {
            if (output.Count >= count)
            {
                return output.ToArray();
            }

            Direction randomDir = (Direction)Random.Range(0, 4);

            if (!output.Contains(randomDir))
            {
                output.Add(randomDir);
            }
        }

        return output.ToArray();
    }

    #endregion

    public static Tile GetTile(string id)
    {
        foreach (Tile t in tileSetStatic.allTiles)
        {
            if (t.id.Equals(id))
            {
                return t;
            }
        }

        throw new System.Exception("No tile with id of " + id);
    }

    #region Debug

    public void PrintInfo()
    {
        string connections = "";

        foreach (Direction dir in this.connections)
        {
            connections += dir + " ";
        }
        Debug.LogFormat("Size: {0} | Connections: {1}", roomSize, connections);
    }

    #endregion
}
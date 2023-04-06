using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] Tilemap tileMap;

    [Space(10)]

    Vector2Int roomSize;
    Direction[] connections;

    TileSet tileSet;

    #region Init
    public void RoomInit(TileSet tileSet)
    {
        this.tileSet = tileSet;

        //numbers init
        roomSize = new Vector2Int(Random.Range(5, 20), Random.Range(5, 20));
        connections = MakeDirections();

        FillRoom(roomSize);
        RemoveExits();

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

    void RemoveExits()
    {
        int exitSize = 3;

        foreach (Direction dir in connections)
        {
            if (dir == Direction.North || dir == Direction.South)
            {
                int exitX = Random.Range(0, roomSize.x - exitSize - 1);

                if (dir == Direction.North)
                {
                    RemoveHorzLine(new Vector3Int(exitX, roomSize.y - 1), exitSize);
                }
                else
                {
                    RemoveHorzLine(new Vector3Int(exitX, 0), exitSize);
                }
            }
            else if (dir == Direction.East || dir == Direction.West)
            {
                int exitY = Random.Range(0, roomSize.y - exitSize - 1);

                if (dir == Direction.West)
                {
                    RemoveVertLine(new Vector3Int(roomSize.x - 1, exitY), exitSize);
                }
                else
                {
                    RemoveVertLine(new Vector3Int(0, exitY), exitSize);
                }
            }
        }
    }

    #endregion

    #region Setter
    void FillRoom(Vector2Int roomSize)
    {
        for (int y = 0; y < roomSize.y; y++)
        {
            for (int x = 0; x < roomSize.x; x++)
            {
                Vector3Int current = new Vector3Int(x, y);

                SetTile(current, "Wall");
            }
        }
    }

    void RemoveHorzLine(Vector3Int start, int length)
    {
        for (int i = 0; i < length; i++)
        {
            var current = new Vector3Int(start.x + i, start.y);
            SetTile(current, "Empty");
        }
    }

    void RemoveVertLine(Vector3Int start, int length)
    {
        for (int i = 0; i < length; i++)
        {
            var current = new Vector3Int(start.x, start.y + i);
            SetTile(current, "Empty");
        }
    }

    void SetTile(Vector3Int cord, string id)
    {
        tileMap.SetTile(cord, GetTileBase(id));
    }
    #endregion

    #region Getter
    TileBase GetTileBase(string id)
    {
        for (int i = 0; i < tileSet.allTiles.Length; i++)
        {
            var current = tileSet.allTiles[i];

            if (current.name.Equals(id))
            {
                return current.tileBase;
            }
        }

        throw new System.Exception("No tile with id of " + id);
    }
    #endregion

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
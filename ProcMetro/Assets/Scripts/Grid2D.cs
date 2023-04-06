using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid2D<T>
{
    T[] data;

    public Vector3Int Size { get; private set; }
    public Vector3Int Offset { get; set; }

    public Grid2D(Vector2Int size, Vector3Int offset)
    {
        Size = new Vector3Int(size.x, size.y);
        Offset = offset;

        data = new T[size.x * size.y];
    }

    public Grid2D(int size, Vector3Int offset)
    {
        Size = new Vector3Int(size, size);
        Offset = offset;

        data = new T[size * size];
    }

    public int GetIndex(Vector3Int pos)
    {
        return pos.x + (Size.x * pos.y);
    }

    public bool InBounds(Vector3Int pos)
    {
        var containsCords = new Vector2Int(pos.x + Offset.x, pos.y + Offset.y);
        return new RectInt(Vector2Int.zero, new Vector2Int(Size.x, Size.y)).Contains(containsCords);
    }

    public T this[int x, int y]
    {
        get
        {
            return this[new Vector3Int(x, y)];
        }
        set
        {
            this[new Vector3Int(x, y)] = value;
        }
    }

    public T this[Vector3Int pos]
    {
        get
        {
            pos += Offset;
            return data[GetIndex(pos)];
        }
        set
        {
            pos += Offset;
            data[GetIndex(pos)] = value;
        }
    }
}
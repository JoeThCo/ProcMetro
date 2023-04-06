using Priority_Queue;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomPathFinding
{
    public struct PathCost
    {
        public bool traversable;
        public float cost;
    }

    static readonly Vector2Int[] neighbors = {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
    };

    Grid2D<Tile> grid;
    SimplePriorityQueue<Tile, float> queue;
    HashSet<Tile> closed;
    Stack<Vector3Int> stack;

    public RoomPathFinding(Grid2D<Tile> grid)
    {
        this.grid = grid;

        queue = new SimplePriorityQueue<Tile, float>();
        closed = new HashSet<Tile>();
        stack = new Stack<Vector3Int>();
    }

    void ResetNodes()
    {
        var size = grid.Size;

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Tile tile = grid[x, y];
                tile.Previous = null;
                tile.Cost = float.PositiveInfinity;
            }
        }
    }

    List<Vector3Int> FindPath(Vector3Int start, Vector3Int end, Func<Tile, Tile, PathCost> costFunction)
    {
        ResetNodes();

        queue = new SimplePriorityQueue<Tile, float>();
        closed = new HashSet<Tile>();
        stack = new Stack<Vector3Int>();

        grid[start].Cost = 0;
        queue.Enqueue(grid[start], 0);

        while (queue.Count > 0)
        {
            Tile node = queue.Dequeue();
            closed.Add(node);

            if (node.Position == end)
            {
                return ReconstructPath(node);
            }

            foreach (var offset in neighbors)
            {
                var offsetVec3Int = new Vector3Int(offset.x, offset.y);

                if (!grid.InBounds(node.Position + offsetVec3Int)) continue;
                var neighbor = grid[node.Position + offsetVec3Int];
                if (closed.Contains(neighbor)) continue;

                var pathCost = costFunction(node, neighbor);
                if (!pathCost.traversable) continue;

                float newCost = node.Cost + pathCost.cost;

                if (newCost < neighbor.Cost)
                {
                    neighbor.Previous = node;
                    neighbor.Cost = newCost;

                    if (queue.TryGetPriority(node, out float existingPriority))
                    {
                        queue.UpdatePriority(node, newCost);
                    }
                    else
                    {
                        queue.Enqueue(neighbor, neighbor.Cost);
                    }
                }
            }
        }

        return null;
    }

    public List<Vector3Int> GetPath(Vector3Int start, Vector3Int endPos)
    {
        return FindPath(start, endPos, (Tile a, Tile b) =>
         {
             var pathCost = new PathCost();

             pathCost.cost = Vector3Int.Distance(b.Position, endPos);    //heuristic

             if (grid[b.Position].tileType == TileType.Wall)
             {
                 pathCost.cost += 1;
             }
             else if (grid[b.Position].tileType == TileType.Empty)
             {
                 pathCost.cost += 5;
             }

             pathCost.traversable = true;

             return pathCost;
         });
    }

    List<Vector3Int> ReconstructPath(Tile node)
    {
        List<Vector3Int> result = new List<Vector3Int>();

        while (node != null)
        {
            stack.Push(node.Position);
            node = node.Previous;
        }

        while (stack.Count > 0)
        {
            result.Add(stack.Pop());
        }

        return result;
    }
}
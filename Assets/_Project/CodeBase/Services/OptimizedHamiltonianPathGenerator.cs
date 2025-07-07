using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services
{
  public class OptimizedHamiltonianPathGenerator : IHamiltonianPathGenerator
  {
    private static readonly Vector2Int[] Directions = {
      new(0, 1),
      new(1, 0),
      new(0, -1),
      new(-1, 0)
    };

    public Vector2Int[] GenerateHamiltonianPath(int gridSize)
    {
      bool[,] visited = new bool[gridSize, gridSize];
      List<Vector2Int> path = new List<Vector2Int>();

      Vector2Int start = new Vector2Int(Random.Range(0, gridSize), Random.Range(0, gridSize));
      path.Add(start);
      visited[start.x, start.y] = true;

      return DFS(start, visited, path, gridSize) 
        ? path.ToArray() 
        : GenerateHamiltonianPath(gridSize);
    }

    private static bool DFS(Vector2Int current, bool[,] visited, List<Vector2Int> path, int gridSize)
    {
      if (path.Count == gridSize * gridSize)
        return true;

      Vector2Int[] shuffled = ShuffleDirections();

      foreach (var dir in shuffled)
      {
        Vector2Int next = current + dir;

        if (IsInside(next, gridSize) && !visited[next.x, next.y])
        {
          visited[next.x, next.y] = true;
          path.Add(next);

          if (DFS(next, visited, path, gridSize))
            return true;

          visited[next.x, next.y] = false;
          path.RemoveAt(path.Count - 1);
        }
      }

      return false;
    }

    private static bool IsInside(Vector2Int pos, int gridSize) => 
      pos.x >= 0 && pos.y >= 0 && pos.x < gridSize && pos.y < gridSize;

    private static Vector2Int[] ShuffleDirections()
    {
      var shuffled = (Vector2Int[])Directions.Clone();
      
      for (int i = 0; i < shuffled.Length; i++)
      {
        int j = Random.Range(i, shuffled.Length);
        (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
      }

      return shuffled;
    }
  }
}
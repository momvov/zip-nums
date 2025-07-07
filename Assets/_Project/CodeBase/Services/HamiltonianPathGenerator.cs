using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services
{
  public class HamiltonianPathGenerator : IHamiltonianPathGenerator
  {
    public Vector2Int[] GenerateHamiltonianPath(int size)
    {
      var path = new List<Vector2Int>();
      var visited = new bool[size, size];

      var directions = new Vector2Int[]
      {
        new(0, 1),
        new(0, -1),
        new(-1, 0),
        new(1, 0)
      };

      Vector2Int start = new Vector2Int(
        Random.Range(0, size),
        Random.Range(0, size)
      );

      path.Add(start);
      visited[start.x, start.y] = true;

      return FindPath(start, path, visited, size, directions) ? path.ToArray() : null;
    }

    private static bool FindPath(
      Vector2Int current, 
      List<Vector2Int> path, 
      bool[,] visited, 
      int size,
      Vector2Int[] directions)
    {
      if (path.Count == size * size)
        return true;

      Shuffle(directions);

      foreach (var dir in directions)
      {
        Vector2Int next = current + dir;

        if (IsInBounds(next, size) && !visited[next.x, next.y])
        {
          visited[next.x, next.y] = true;
          path.Add(next);

          if (FindPath(next, path, visited, size, directions))
            return true;

          visited[next.x, next.y] = false;
          path.RemoveAt(path.Count - 1);
        }
      }

      return false;
    }

    private static bool IsInBounds(Vector2Int pos, int size) => 
      pos.x >= 0 && pos.x < size && pos.y >= 0 && pos.y < size;

    private static void Shuffle(Vector2Int[] array)
    {
      for (int i = array.Length - 1; i > 0; i--)
      {
        int j = Random.Range(0, i + 1);
        (array[i], array[j]) = (array[j], array[i]);
      }
    }
  }
}
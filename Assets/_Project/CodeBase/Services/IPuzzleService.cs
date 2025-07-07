using System;
using System.Collections.Generic;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Services
{
  public interface IPuzzleService
  {
    IReadOnlyCollection<Vector2Int> PathPoints { get; }
    LevelData LevelData { get; }

    event Action PathChanged;
    public event Action<Vector2Int> PathPointRemoved;
    public event Action<Vector2Int> PathPointAdded;
    event Action Solved;
    event Action LevelDataSet;

    void InitializeWithLevel(LevelData levelData);
    bool SetNewPathPoint(Vector2Int newPoint);
    void ClearPath();
    Vector2Int GetPathPointByIndex(int index);
  }
}
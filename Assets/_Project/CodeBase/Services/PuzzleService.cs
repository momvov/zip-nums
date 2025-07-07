using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Services
{
  public class PuzzleService : IPuzzleService
  {
    public IReadOnlyCollection<Vector2Int> PathPoints => _pathPoints;
    public LevelData LevelData => _levelData;

    public event Action PathChanged;
    public event Action<Vector2Int> PathPointRemoved;
    public event Action<Vector2Int> PathPointAdded;
    public event Action LevelDataSet;
    public event Action Solved;

    private readonly List<Vector2Int> _pathPoints = new();
    private LevelData _levelData;

    public void InitializeWithLevel(LevelData levelData)
    {
      _levelData = levelData;
      
      ClearPath();
      
      LevelDataSet?.Invoke();
    }

    public bool SetNewPathPoint(Vector2Int newPoint)
    {
      bool success = false;
      
      if (_pathPoints.Count > 1 &&
          newPoint == _pathPoints[_pathPoints.Count - 2])
      {
        Vector2Int removedPathPoint = _pathPoints[_pathPoints.Count - 1];
        _pathPoints.RemoveAt(_pathPoints.Count - 1);
        PathPointRemoved?.Invoke(removedPathPoint);
        PathChanged?.Invoke();
        success = true;
      }

      if (Mathf.Approximately(Vector2.Distance(newPoint, _pathPoints.Last()), 1) &&
          !_pathPoints.Contains(newPoint))
      {
        _pathPoints.Add(newPoint);
        PathPointAdded?.Invoke(newPoint);
        PathChanged?.Invoke();
        success = true;
      }
      
      if (IsSolved()) 
        Solved?.Invoke();

      return success;
    }

    public void ClearPath()
    {
      _pathPoints.Clear();
      _pathPoints.Add(_levelData.SolutionPath[0]);
      
      PathChanged?.Invoke();
    }

    public Vector2Int GetPathPointByIndex(int index) => 
      _pathPoints[index];

    private bool IsSolved()
    {
      if (_pathPoints.Count != _levelData.SolutionPath.Length)
        return false;

      if (_pathPoints[^1] != _levelData.SolutionPath[^1])
        return false;
      
      int numIndex = 0;
      foreach (var step in _pathPoints)
      {
        if (step == _levelData.NumberPositions[numIndex])
        {
          numIndex++;
          if (numIndex == _levelData.NumberPositions.Length)
            break;
        }
      }

      return numIndex == _levelData.NumberPositions.Length;
    }
  }
}
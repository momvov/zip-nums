using System;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
  public class LevelData
  {
    public int GridSize;
    public Vector2Int[] SolutionPath;
    public Vector2Int[] NumberPositions;
  }
}
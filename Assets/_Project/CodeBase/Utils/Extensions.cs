using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Utils
{
  public static class Extensions
  {
    public static Vector2Int GetLastPathPoint(this IPuzzleService puzzleService) => 
      puzzleService.GetPathPointByIndex(puzzleService.PathPoints.Count - 1);
    
    public static Vector2Int GetPenultimatePathPoint(this IPuzzleService puzzleService) => 
      puzzleService.GetPathPointByIndex(puzzleService.PathPoints.Count - 2);
  }
}
using CodeBase.Data;
using CodeBase.StaticData;

namespace CodeBase.Services
{
  public interface ILevelGenerator
  {
    LevelData GenerateLevel(LevelSetupConfig levelSetupConfig);
    LevelData GenerateLevel(int gridSize, int maxNumber = 9, int minDistanceBetweenNums = 1, int maxDistanceBetweenNums = 6);
  }
}
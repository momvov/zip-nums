using CodeBase.Data;
using CodeBase.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Services
{
  public class LevelGenerator : ILevelGenerator
  {
    private readonly IHamiltonianPathGenerator _hamiltonianPathGen;

    public LevelGenerator(IHamiltonianPathGenerator hamiltonianPathGen) => 
      _hamiltonianPathGen = hamiltonianPathGen;

    public LevelData GenerateLevel(LevelSetupConfig levelSetupConfig)
    {
      return GenerateLevel(
        levelSetupConfig.GridSize, 
        levelSetupConfig.MaxNumber, 
        levelSetupConfig.MinDistanceBetweenNums, 
        levelSetupConfig.MaxDistanceBetweenNums);
    }
    
    public LevelData GenerateLevel(
      int gridSize, 
      int maxNumber = 9, 
      int minDistanceBetweenNums = 1, 
      int maxDistanceBetweenNums = 6)
    {
      Vector2Int[] path = _hamiltonianPathGen.GenerateHamiltonianPath(gridSize);
      int pathLength = path.Length;

      float segmentSize = (pathLength - 1f) / (maxNumber - 1);
      int[] indices = new int[maxNumber];
      indices[0] = 0;
      indices[maxNumber - 1] = pathLength - 1;

      for (int i = 1; i < maxNumber - 1; i++)
      {
        float baseIndex = i * segmentSize;

        int maxNoise = Mathf.Min(maxDistanceBetweenNums - minDistanceBetweenNums, Mathf.FloorToInt(segmentSize * 0.3f));
        int noise = Random.Range(-maxNoise, maxNoise + 1);

        indices[i] = Mathf.RoundToInt(baseIndex + noise);
      }

      indices = EnforceStepConstraints(indices, minDistanceBetweenNums, maxDistanceBetweenNums, pathLength);

      var numberPositions = new Vector2Int[maxNumber];
      for (int i = 0; i < maxNumber; i++)
        numberPositions[i] = path[indices[i]];

      return new LevelData
      {
        GridSize = gridSize,
        SolutionPath = path,
        NumberPositions = numberPositions
      };
    }
    
    private int[] EnforceStepConstraints(int[] indices, int minStep, int maxStep, int maxPathLength)
    {
      int n = indices.Length;

      indices[0] = 0;
      indices[n - 1] = maxPathLength - 1;

      for (int i = 1; i < n - 1; i++)
      {
        indices[i] = Mathf.Clamp(indices[i], indices[i - 1] + minStep, indices[n - 1] - minStep * (n - i - 1));

        if (indices[i] - indices[i - 1] > maxStep)
          indices[i] = indices[i - 1] + maxStep;
      }

      for (int i = n - 2; i >= 1; i--) 
        indices[i] = Mathf.Clamp(indices[i], indices[i - 1] + minStep, indices[i + 1] - minStep);

      return indices;
    }
  }
}
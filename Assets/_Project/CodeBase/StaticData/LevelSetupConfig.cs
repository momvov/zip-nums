using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = nameof(LevelSetupConfig), menuName = "StaticData/"+nameof(LevelSetupConfig))]
  public class LevelSetupConfig : ScriptableObject
  {
    public int GridSize = 6;
    public int MaxNumber = 9;

    public int MinDistanceBetweenNums = 1;
    public int MaxDistanceBetweenNums = 6;
  }
}
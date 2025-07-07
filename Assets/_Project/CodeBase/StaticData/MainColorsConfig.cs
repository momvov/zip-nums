using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = nameof(MainColorsConfig), menuName = "StaticData/"+nameof(MainColorsConfig))]
  public class MainColorsConfig : ScriptableObject
  {
    public List<Color> MainColors;
  }
}
using System;
using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Services
{
  public class MainColorProvider
  {
    public event Action<Color> ColorChanged;
    
    private MainColorsConfig _mainColorsConfig;

    private List<Color> _mainColors => _mainColorsConfig.MainColors;

    private Color _currentMainColor;

    public void SetConfig(MainColorsConfig mainColorsConfig) => 
      _mainColorsConfig = mainColorsConfig;

    public Color GetMainColor() => 
      _currentMainColor;

    public void DetermineNewMainColor()
    {
      if (_currentMainColor == default)
      {
        _currentMainColor = _mainColors[Random.Range(0, _mainColors.Count - 1)];
      }
      else
      {
        int currentIndex = _mainColors.IndexOf(_currentMainColor);

        int newIndex = Random.Range(0, _mainColors.Count - 1);
        if (newIndex >= currentIndex)
          newIndex++;

        _currentMainColor = _mainColors[newIndex];
      }
      
      ColorChanged?.Invoke(_currentMainColor);
    }
  }
}
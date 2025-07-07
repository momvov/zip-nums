using System.Collections.Generic;
using CodeBase.Infrastructure;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Views
{
  public class HowToPlayInfoView : MonoBehaviour
  {
    [SerializeField] private List<Image> _imagesToColor = new();
    
    private MainColorProvider _mainColorProvider;
    
    private void Awake()
    {
      _mainColorProvider = ServiceLocator.Get<MainColorProvider>();

      OnColorChanged(_mainColorProvider.GetMainColor());
      _mainColorProvider.ColorChanged += OnColorChanged;
    }

    private void OnColorChanged(Color mainColor)
    {
      foreach (Image image in _imagesToColor) 
        image.color = mainColor;
    }
  }
}
using System.Collections.Generic;
using CodeBase.StaticData;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.Services
{
  public class WindowsService
  {
    private Transform _windowsContainer;
    
    private Dictionary<WindowId, Window> _windows = new();

    public void Initialize(Transform windowsContainer)
    {
      _windowsContainer = windowsContainer;
      
      foreach (Transform child in _windowsContainer)
      {
        if (child.TryGetComponent<Window>(out var window))
          _windows.Add(window.Id, window);
      }
    }
    
    public void ShowWindow(WindowId id)
    {
      if (_windows.TryGetValue(id, out Window window)) 
        window.Show();
    }

    public void HideWindow(WindowId id)
    {
      if (_windows.TryGetValue(id, out Window window)) 
        window.Hide();
    }

    public void HideAllWindows()
    {
      foreach (KeyValuePair<WindowId, Window> window in _windows) 
        window.Value.Hide();
    }
  }
}
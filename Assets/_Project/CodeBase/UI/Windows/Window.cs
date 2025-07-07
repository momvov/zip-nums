using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.UI.Windows
{
  public abstract class Window : MonoBehaviour
  {
    [field: SerializeField] public WindowId Id { get; private set; }
    
    public abstract void Show();
    
    public abstract void Hide();
  }
}
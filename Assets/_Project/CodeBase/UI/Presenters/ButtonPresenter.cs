using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Presenters
{
  public abstract class ButtonPresenter : MonoBehaviour
  {
    private Button _button;

    protected virtual void Awake()
    {
      _button = GetComponent<Button>();
      
      _button.onClick.AddListener(OnClicked);
    }

    protected virtual void OnDestroy() => 
      _button.onClick.RemoveListener(OnClicked);

    protected abstract void OnClicked();
  }
}
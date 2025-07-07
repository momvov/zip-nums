using TMPro;
using UnityEngine;

namespace CodeBase.UI.Views
{
  public class NumPointView : MonoBehaviour
  {
    [field: SerializeField] public RectTransform RectTransform { get; private set; }
    
    [SerializeField] private TextMeshProUGUI _numText;
    
    public void SetNum(int num) => 
      _numText.text = num.ToString();
  }
}
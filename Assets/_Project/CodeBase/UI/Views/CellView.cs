using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.UI.Views
{
  public class CellView : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
  {
    [field: SerializeField] public RectTransform RectTransform { get; private set; }
    
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _colorImage;
    [SerializeField] private Image _borderImage;

    public event Action<PointerEventData> PointerDown;
    public event Action<PointerEventData> PointerEntered;
    
    public void SetFillSprite(Sprite sprite)
    {
      _fillImage.sprite = sprite;
      _colorImage.sprite = sprite;
    }

    public void SetBorderSprite(Sprite sprite) => 
      _borderImage.sprite = sprite;
    
    public void SetColor(Color color) => 
      _colorImage.color = color;
    
    public void OnPointerDown(PointerEventData eventData) => 
      PointerDown?.Invoke(eventData);

    public void OnPointerEnter(PointerEventData eventData) => 
      PointerEntered?.Invoke(eventData);
  }
}
using CodeBase.Infrastructure;
using CodeBase.Services;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Presenters
{
  public class WinTimePresenter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _text;
    
    private TimerService _timerService;

    private void Awake()
    {
      _timerService = ServiceLocator.Get<TimerService>();
    }

    private void OnEnable()
    {
      float time = _timerService.CurrentTime;
      
      int minutes = Mathf.FloorToInt(time / 60);
      int seconds = Mathf.FloorToInt(time % 60);
      int milliseconds = Mathf.FloorToInt((time * 100) % 100);
      
      _text.text = $"Your time is: {minutes:00}:{seconds:00}:{milliseconds:00}";
    }
  }
}
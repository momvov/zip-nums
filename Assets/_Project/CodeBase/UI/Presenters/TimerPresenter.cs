using CodeBase.Infrastructure;
using CodeBase.Services;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Presenters
{
  public class TimerPresenter : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _timerText;
    
    private TimerService _timerService;
    
    private void Awake()
    {
      _timerService = ServiceLocator.Get<TimerService>();

      _timerService.TimeChanged += OnTimeChanged;
    }

    private void OnTimeChanged(float time)
    {
      int minutes = Mathf.FloorToInt(time / 60);
      int seconds = Mathf.FloorToInt(time % 60);
      int milliseconds = Mathf.FloorToInt((time * 100) % 100);
      _timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
  }
}
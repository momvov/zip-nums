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
      _text.text = $"Your time is: {_timerService.CurrentTime}";
    }
  }
}
using System;
using System.Collections;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Services
{
  public class TimerService
  {
    public float CurrentTime { get; private set; }
    public bool IsRunning => timerCoroutine != null;
    
    public event Action<float> TimeChanged;

    private Coroutine timerCoroutine;

    public void Start()
    {
      if (IsRunning)
        return;
      
      timerCoroutine = CoroutineRunner.Instance.BeginCoroutine(StopwatchCoroutine());
    }

    public void Stop()
    {
      CoroutineRunner.Instance.EndCoroutine(timerCoroutine);
      timerCoroutine = null;
    }
    
    public void Reset()
    {
      Stop();
      CurrentTime = 0;
      TimeChanged?.Invoke(CurrentTime);
    }

    private IEnumerator StopwatchCoroutine()
    {
      while (true)
      {
        CurrentTime += Time.deltaTime;
        TimeChanged?.Invoke(CurrentTime);
        yield return null;
      }
    }
  }
}
using System.Collections;
using UnityEngine;

namespace CodeBase.UI.Windows
{
  public class FadedWindow : Window
  {
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private CanvasGroup _canvasGroup;
    private Coroutine activeFadeCoroutine;

    public override void Show()
    {
      gameObject.SetActive(true);
      
      if (activeFadeCoroutine != null) 
        StopCoroutine(activeFadeCoroutine);
      
      activeFadeCoroutine = StartCoroutine(Fade(1f));
    }

    public override void Hide()
    {
      if (activeFadeCoroutine != null) 
        StopCoroutine(activeFadeCoroutine);
      
      activeFadeCoroutine = StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
      _canvasGroup.interactable = (targetAlpha == 1);
      _canvasGroup.blocksRaycasts = (targetAlpha == 1);

      float startAlpha = _canvasGroup.alpha;
      float elapsedTime = 0f;

      while (elapsedTime < _fadeDuration)
      {
        elapsedTime += Time.unscaledDeltaTime;
        float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / _fadeDuration);
        _canvasGroup.alpha = newAlpha;
        yield return null;
      }

      _canvasGroup.alpha = targetAlpha;

      if (targetAlpha == 0) 
        gameObject.SetActive(false);
    }
  }
}
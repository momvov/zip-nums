using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class CoroutineRunner : MonoBehaviour
  {
    private static CoroutineRunner instance;

    public static CoroutineRunner Instance
    {
      get
      {
        if (instance == null)
        {
          GameObject go = new GameObject("[CoroutineRunner]");
          instance = go.AddComponent<CoroutineRunner>();
        }
        return instance;
      }
    }

    private void Awake()
    {
      if (instance != null && instance != this)
      {
        Destroy(gameObject);
        return;
      }

      instance = this;
      DontDestroyOnLoad(gameObject);
    }
    
    public Coroutine BeginCoroutine(IEnumerator coroutine) => 
      StartCoroutine(coroutine);

    public void EndCoroutine(Coroutine coroutine)
    {
      if (coroutine != null) 
        StopCoroutine(coroutine);
    }
  }
}
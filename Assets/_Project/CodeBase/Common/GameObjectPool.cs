using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Common
{
  public class GameObjectPool<T> where T : Component
  {
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly Stack<T> _pool;

    public GameObjectPool(T prefab, int initialSize, Transform parent = null)
    {
      _prefab = prefab;
      _parent = parent;
      _pool = new Stack<T>(initialSize);

      for (int i = 0; i < initialSize; i++)
      {
        T obj = CreateObject();
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
      }
    }

    public T Get()
    {
      var obj = _pool.Count > 0 
        ? _pool.Pop() 
        : CreateObject();

      obj.gameObject.SetActive(true);
      return obj;
    }

    public void Release(T obj)
    {
      obj.gameObject.SetActive(false);
      _pool.Push(obj);
    }

    private T CreateObject() => 
      Object.Instantiate(_prefab, _parent);
  }
}
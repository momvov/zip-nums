using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public static class ServiceLocator
  {
    private static readonly Dictionary<Type, object> services = new();

    public static void Register<T>(T service)
    {
      var type = typeof(T);
      if (!services.TryAdd(type, service)) 
        Debug.LogError($"[ServiceLocator] Service of type {type.Name} is already registered.");
    }

    public static T Get<T>()
    {
      var type = typeof(T);
      if (!services.TryGetValue(type, out var service))
        throw new Exception($"[ServiceLocator] Service of type {type.Name} is not registered.");
      
      return (T)service;
    }
    
    public static void Unregister<T>()
    {
      var type = typeof(T);
      if (!services.ContainsKey(type))
      {
        Debug.LogWarning($"[ServiceLocator] Trying to unregister a non-existent service of type {type.Name}.");
        return;
      }
      services.Remove(type);
    }
    
    public static void Clear() => 
      services.Clear();
  }
}
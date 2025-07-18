using System;
using System.Collections.Generic;
using quik.Runtime.Pooling.Interfaces;
using UnityEngine;

namespace quik.Runtime.Pooling
{
    /// <summary>
    /// Central registry and dispatcher for object pools identified by string keys.
    /// Supports both MonoBehaviour-based and plain C# object pools.
    /// </summary>
    public class PoolManager : IPoolManager
    {
        private readonly Dictionary<string, object> _pools = new();

        public void Register<T>(string key, IPool<T> pool)
        {
            if (!_pools.TryAdd(key, pool))
            {
                Debug.LogWarning($"[PoolManager] Pool with key {key} already registered.");
            }
        }

        public T Get<T>(string key)
        {
            if (_pools.TryGetValue(key, out var poolObj) && poolObj is IPool<T> pool)
            {
                return pool.Get();
            }

            throw new InvalidOperationException($"[PoolManager] No pool found with key: {key}");
        }

        public void Release<T>(string key, T instance)
        {
            if (_pools.TryGetValue(key, out var poolObj) && poolObj is IPool<T> pool)
            {
                pool.Release(instance);
            }
            else
            {
                Debug.LogWarning($"[PoolManager] No pool found for release with key: {key}");
            }
        }

        public bool Contains(string key) => _pools.ContainsKey(key);
    }
}

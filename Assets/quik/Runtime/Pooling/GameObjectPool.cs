using System;
using UnityEngine;
using quik.Runtime.Pooling.Interfaces;

namespace quik.Runtime.Pooling
{
    /// <summary>
    /// A Unity-specific object pool for MonoBehaviour-based objects.
    /// Automatically instantiates from a prefab and handles activation, deactivation, and optional parenting.
    /// </summary>
    public class GameObjectPool<T> : IPool<T> where T : MonoBehaviour
    {
        private readonly ObjectPool<T> _internalPool;

        /// <summary>
        /// Initializes the pool with a prefab and optionally preloads instances.
        /// </summary>
        /// <param name="prefab">The MonoBehaviour prefab to instantiate.</param>
        /// <param name="preloadCount">Number of instances to preload into the pool.</param>
        /// <param name="parent">Optional parent transform for organizing pooled objects in the hierarchy.</param>
        public GameObjectPool(T prefab, int preloadCount = 0, Transform parent = null)
        {
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab), "Prefab cannot be null");
            }

            _internalPool = new ObjectPool<T>(
                factory: () => UnityEngine.Object.Instantiate(prefab, parent),
                onGet: item => item.gameObject.SetActive(true),
                onRelease: item => item.gameObject.SetActive(false)
            );

            if (preloadCount > 0)
            {
                _internalPool.Preload(preloadCount);
            }
        }

        public T Get()
        {
            return _internalPool.Get();
        }

        /// <summary>
        /// Returns an instance to the pool and deactivates it.
        /// </summary>
        /// <param name="item">The object to return to the pool.</param>
        public void Release(T item)
        {
            _internalPool.Release(item);
        }

        /// <summary>
        /// Preloads the specified number of instances into the pool.
        /// </summary>
        /// <param name="count">The number of instances to preload.</param>
        public void Preload(int count)
        {
            _internalPool.Preload(count);
        }

        /// <summary>
        /// Gets the current number of available (inactive) objects in the pool.
        /// </summary>
        public int Count => _internalPool.Count;
    }
}

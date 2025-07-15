using System;
using System.Collections.Generic;
using quik.Runtime.Pooling.Interfaces;

namespace quik.Runtime.Pooling
{
    /// <summary>
    /// A generic object pool for managing reusable instances of class-type objects.
    /// Helps reduce memory allocations and improve performance by reusing objects instead of creating new ones.
    /// </summary>
    public class ObjectPool<T> : IPool<T> where T : class
    {
        private readonly Stack<T> _pool = new();
        private readonly Func<T> _factory;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onRelease;
        
        public int Count => _pool.Count;
        
        /// <summary>
        /// Creates a new object pool with a factory function and optional callbacks.
        /// </summary>
        /// <param name="factory">Function to create new instances when the pool is empty.</param>
        /// <param name="onGet">Optional callback invoked when an object is retrieved from the pool.</param>
        /// <param name="onRelease">Optional callback invoked when an object is returned to the pool.</param>
        public ObjectPool(Func<T> factory, Action<T> onGet = null, Action<T> onRelease = null)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _onGet = onGet;
            _onRelease = onRelease;
        }

        public T Get()
        {
            var item = _pool.Count > 0 ? _pool.Pop() : _factory();
            _onGet?.Invoke(item);

            if (item is IPoolable poolable)
            {
                poolable.OnGetFromPool();
            }

            return item;
        }
        
        public void Release(T item)
        {
            _onRelease?.Invoke(item);
            _pool.Push(item);
            
            if (item is IPoolable poolable)
            {
                poolable.OnReturnToPool();
            }
        }
        
        public void Preload(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _pool.Push(_factory());
            }
        }
    }
}

using System;

namespace quik.Runtime.Pooling.Interfaces
{
    public interface IPoolManager
    {
        /// <summary>
        /// Registers a pool with the specified key.
        /// If a pool with the same key already exists, a warning is logged.
        /// </summary>
        /// <typeparam name="T">The type of objects managed by the pool.</typeparam>
        /// <param name="key">The unique key to identify this pool.</param>
        /// <param name="pool">The pool instance to register.</param>
        void Register<T>(string key, IPool<T> pool);
        
        /// <summary>
        /// Retrieves an object from the pool associated with the given key.
        /// Throws if the key is not found or if the type is incorrect.
        /// </summary>
        /// <typeparam name="T">The type of object to retrieve.</typeparam>
        /// <param name="key">The key identifying the pool.</param>
        /// <returns>An object instance from the pool.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the key is not registered or the type mismatches.</exception>
        T Get<T>(string key);
        
        /// <summary>
        /// Returns an object back to the pool it came from.
        /// Logs a warning if the key is not found or mismatched.
        /// </summary>
        /// <typeparam name="T">The type of the object being returned.</typeparam>
        /// <param name="key">The key identifying the pool.</param>
        /// <param name="instance">The object instance to release.</param>
        void Release<T>(string key, T instance);
        
        /// <summary>
        /// Checks whether a pool is registered under the given key.
        /// </summary>
        /// <param name="key">The key to look for.</param>
        /// <returns>True if a pool is registered with the given key; otherwise, false.</returns>
        bool Contains(string key);
    }
}
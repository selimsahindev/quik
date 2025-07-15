namespace quik.Runtime.Pooling.Interfaces
{
    /// <summary>
    /// Defines the contract for a generic object pool.
    /// </summary>
    public interface IPool<T>
    {
        /// <summary>
        /// Gets an instance from the pool. Instantiates a new one if the pool is empty.
        /// </summary>
        /// <returns>An active instance of the pooled object.</returns>
        T Get();

        /// <summary>
        /// Returns an object to the pool, making it available for reuse.
        /// </summary>
        /// <param name="item">The object to return to the pool.</param>
        void Release(T item);

        /// <summary>
        /// Pre-instantiates and stores a specified number of objects in the pool.
        /// Useful for avoiding runtime instantiation spikes.
        /// </summary>
        /// <param name="count">Number of instances to preload.</param>
        void Preload(int count);
        
        /// <summary>
        /// Gets the number of available (inactive) objects in the pool.
        /// </summary>
        int Count { get; }
    }
}

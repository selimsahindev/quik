namespace quik.Runtime.Pooling.Interfaces
{
    /// <summary>
    /// Interface for objects that want to respond to pooling events.
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// Called when the object is fetched from the pool.
        /// </summary>
        void OnGetFromPool();
        
        /// <summary>
        /// Called when the object is fetched from the pool.
        /// </summary>
        void OnReturnToPool();
    }
}

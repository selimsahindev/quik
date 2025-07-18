namespace quik.Runtime.SaveSystem.Interfaces
{
    /// <summary>
    /// Defines a generic interface for saving and loading data persistently using string keys.
    /// </summary>
    public interface ISaveService
    {
        /// <summary>
        /// Saves the given data under the specified key.
        /// </summary>
        /// <param name="key">The unique identifier to store the data under.</param>
        /// <param name="data">The data to save.</param>
        void Save<T>(string key, T data);

        /// <summary>
        /// Loads data of type T associated with the given key. 
        /// Returns the default game data if the key does not exist.
        /// </summary>
        /// <param name="key">The key to retrieve data from.</param>
        /// <returns>The deserialized data of type T.</returns>
        T Load<T>(string key);
        
        /// <summary>
        /// Checks whether a key exists in the storage system.
        /// </summary>
        /// <param name="key">The key to check for existence.</param>
        /// <returns>True if the key exists; otherwise, false.</returns>
        bool HasKey(string key);
        
        /// <summary>
        /// Deletes the data associated with the specified key.
        /// </summary>
        /// <param name="key">The key to delete.</param>
        void Delete(string key);
        
        /// <summary>
        /// Deletes all stored data.
        /// </summary>
        void DeleteAll();
    }
}

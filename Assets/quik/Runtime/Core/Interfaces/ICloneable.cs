namespace quik.Runtime.Core.Interfaces
{
    /// <summary>
    /// Provides a mechanism for creating a deep copy of an object.
    /// </summary>
    public interface ICloneable<out T>
    {
        /// <summary>
        /// Creates a deep copy of the current object instance.
        /// </summary>
        /// <returns>A new instance of <typeparamref name="T"/> that is a deep copy of the current object.</returns>
        T Clone();
    }
}
namespace quik.Runtime.ServiceProvider.Interfaces
{
    /// <summary>
    /// Defines a contract for classes that support dependency injection via an external service provider.
    /// Implementing classes receive their dependencies through the Inject method.
    /// </summary>
    public interface IInjectable
    {
        /// <summary>
        /// Injects dependencies into the implementing class using the provided service provider.
        /// </summary>
        /// <param name="provider">The service provider to resolve dependencies from.</param>
        void Inject(IServiceProvider provider);
    }
}
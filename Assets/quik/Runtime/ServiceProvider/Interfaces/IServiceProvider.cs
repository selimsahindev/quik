using System;

namespace quik.Runtime.ServiceProvider.Interfaces
{
    public interface IServiceProvider
    {
        /// <summary>
        /// Registers an existing service instance with the given interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type to register the service under.</typeparam>
        /// <param name="implementation">The service instance that implements the interface.</param>
        void Register<TInterface>(object implementation);
        
        /// <summary>
        /// Instantiates and registers a new service of the specified concrete type under the given interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type to register the service under.</typeparam>
        /// <typeparam name="TConcrete">The concrete class that implements the interface. Must have a parameterless constructor.</typeparam>
        void Register<TInterface, TConcrete>() where TConcrete : TInterface, new();
        
        /// <summary>
        /// Registers a service instance with the specified interface type dynamically.
        /// </summary>
        /// <param name="interfaceType">The interface type to register the service under.</param>
        /// <param name="implementation">The service instance that implements the specified interface.</param>
        /// <remarks>
        /// This method allows you to register services using a runtime-determined interface type. It ensures that the service
        /// implements the specified interface and that the interface type is valid.
        /// </remarks>
        void Register(Type interfaceType, object implementation);
        
        /// <summary>
        /// Unregisters the service associated with the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type of the service to remove.</typeparam>
        void Unregister<TInterface>();
        
        /// <summary>
        /// Attempts to resolve the service registered under the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type of the service.</typeparam>
        /// <param name="result">The resolved service instance, or default if not found.</param>
        /// <returns>True if the service was found; otherwise, false.</returns>
        bool TryResolve<TInterface>(out TInterface result);
        
        /// <summary>
        /// Resolves and returns the service registered under the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type of the service.</typeparam>
        /// <returns>The resolved service instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no service is registered for the given type.</exception>
        TInterface Resolve<TInterface>();

        /// <summary>
        /// Checks whether a service is registered under the specified interface type.
        /// </summary>
        /// <typeparam name="TInterface">The interface type to check.</typeparam>
        /// <returns>True if the service is registered; otherwise, false.</returns>
        bool Contains(Type type);
    }
}
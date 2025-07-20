using System;
using quik.Runtime.Core;
using UnityEngine;
using IServiceProvider = quik.Runtime.Services.Interfaces.IServiceProvider;

namespace quik.Runtime.Services
{
    /// <summary>
    /// Provides global access to services via a centralized service provider.
    /// Used to register, resolve, and manage dependencies across the entire application.
    /// 
    /// Initialize it once via <see cref="Init"/> (typically in a bootstrap scene),
    /// and access services anywhere using static methods like <see cref="Resolve{T}"/>.
    /// </summary>
    public class ServiceLocator : MonoSingleton<ServiceLocator>
    {
        private IServiceProvider _provider;
        
        /// <summary>
        /// Initializes the global service provider. Should only be called once, typically from Bootstrapper.
        /// </summary>
        public static void Init(IServiceProvider provider)
        {
            if (Instance._provider != null)
            {
                if (ReferenceEquals(Instance._provider, provider))
                {
                    return;
                }

                Debug.LogWarning($"[{nameof(ServiceLocator)}] Already initialized with a different provider.");
                return;
            }

            Instance._provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        
        /// <summary>
        /// Resolves a service of the specified interface type.
        /// </summary>
        public static T Resolve<T>() => Require().Resolve<T>();

        /// <summary>
        /// Attempts to resolve a service, returns true if successful.
        /// </summary>
        public static bool TryResolve<T>(out T service) => Require().TryResolve(out service);

        /// <summary>
        /// Returns true if a service of the specified type has been registered.
        /// </summary>
        public static bool Contains(Type type) => Require().Contains(type);

        /// <summary>
        /// Registers a new service with the global provider.
        /// </summary>
        public static void Register<TInterface>(object implementation) => Require().Register<TInterface>(implementation);

        /// <summary>
        /// Unregisters a service from the global provider.
        /// </summary>
        public static void Unregister<TInterface>() => Require().Unregister<TInterface>();

        /// <summary>
        /// Throws if provider is not initialized, otherwise returns it.
        /// </summary>
        private static IServiceProvider Require()
        {
            if (Instance._provider == null)
            {
                throw new InvalidOperationException($"[{nameof(ServiceLocator)}] Provider not initialized. Call Init() first.");
            }

            return Instance._provider;
        }
    }
}
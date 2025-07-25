using System;
using System.Collections.Generic;
using quik.Runtime.Services.Enums;
using UnityEngine;
using IServiceProvider = quik.Runtime.Services.Interfaces.IServiceProvider;

namespace quik.Runtime.Services
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, object> _services = new();
        private readonly ProviderScope _scope;
        
        private ServiceProvider(ProviderScope scope)
        {
            _scope = scope;
        }

        public static ServiceProvider Create(ProviderScope scope)
        {
            return new ServiceProvider(scope);
        }

        public void Register<TInterface>(object implementation)
        {
            var interfaceType = typeof(TInterface);

            if (!IsValidImplementation(interfaceType, implementation))
            {
                return;
            }
            
            _services[interfaceType] = implementation;
        }
        
        public void Register<TInterface, TConcrete>() where TConcrete : TInterface, new()
        {
            Register<TInterface>(new TConcrete());
        }
        
        public void Register(Type interfaceType, object implementation)
        {
            if (!IsValidImplementation(interfaceType, implementation))
            {
                return;
            }

            _services[interfaceType] = implementation;
        }
        
        public void Unregister<TInterface>()
        {
            _services.Remove(typeof(TInterface));
        }
        
        public bool TryResolve<TInterface>(out TInterface implementation)
        {
            try
            {
                implementation = Resolve<TInterface>();
                return true;
            }
            catch (ServiceNotFoundException)
            {
                implementation = default;
                return false;
            }
        }
        
        public TInterface Resolve<TInterface>()
        {
            if (_services.TryGetValue(typeof(TInterface), out var value))
            {
                return (TInterface)value;
            }

            // Fallback: try to resolve the service from the global (application-wide) service locator.
            // Note: Avoid resolving via the service locator if this is the global provider, as it would cause an infinite recursion.
            var isSceneProvider = _scope == ProviderScope.Scene;
            if (isSceneProvider && ServiceLocator.TryResolve<TInterface>(out var service))
            {
                return service;
            }
            
            throw new ServiceNotFoundException(typeof(TInterface));
        }
        
        public bool Contains(Type type)
        {
            return _services.ContainsKey(type);
        }
        
        private bool IsValidImplementation(Type interfaceType, object implementation)
        {
            if (!interfaceType.IsInterface)
            {
                Debug.LogWarning($"[ServiceProvider] {interfaceType.Name} is not an interface.");
                return false;
            }

            if (!interfaceType.IsAssignableFrom(implementation.GetType()))
            {
                Debug.LogError($"[ServiceProvider] {implementation.GetType().Name} does not implement {interfaceType.Name}.");
                return false;
            }

            return true;
        }
    }
}
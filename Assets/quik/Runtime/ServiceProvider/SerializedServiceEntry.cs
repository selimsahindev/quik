using System;
using System.Linq;
using quik.Runtime.ServiceProvider.Interfaces;
using UnityEditor;
using UnityEngine;
using IServiceProvider = quik.Runtime.ServiceProvider.Interfaces.IServiceProvider;

namespace quik.Runtime.ServiceProvider
{
    [Serializable]
    public class SerializedServiceEntry
    {
        [Tooltip("Must implement at least one known interface.")]
        public MonoScript script;
        
        [Header("Optional"), Tooltip("Optional override for interface type if multiple are implemented")]
        public string interfaceTypeName;

        /// <summary>
        /// Registers the service to the provider with interface-based registration.
        /// </summary>
        /// <param name="provider">The service provider to register the service with.</param>
        public void RegisterTo(IServiceProvider provider)
        {
            if (!TryGetInterfaceType(out var interfaceType))
            {
                Debug.LogWarning($"[{nameof(SerializedServiceEntry)}] script doesn't implement a usable interface.");
                return;
            }
            
            var concreteType = script.GetClass();
            if (concreteType == null)
            {
                Debug.LogWarning($"[{nameof(SerializedServiceEntry)}] Script has no valid class.");
                return;
            }
            
            // Create instance via Activator
            object instance;
            
            try
            {
                instance = Activator.CreateInstance(concreteType);
            }
            catch (Exception e)
            {
                Debug.LogError($"[{nameof(SerializedServiceEntry)}] Failed to create instance of {interfaceType.Name}: {e.Message}");
                return;
            }

            if (instance == null)
            {
                Debug.LogError($"[{nameof(SerializedServiceEntry)}] Could not instantiate {interfaceType.Name}");
                return;
            }
            
            provider.Register(interfaceType, instance);
            Debug.Log($"[{nameof(SerializedServiceEntry)}] Registered {interfaceType.Name} from script {interfaceType.Name}");
        }
        
        public bool TryGetInterfaceType(out Type interfaceType)
        {
            interfaceType = null;
            
            if (script == null)
            {
                return false;
            }
            
            if (!string.IsNullOrEmpty(interfaceTypeName))
            {
                interfaceType = Type.GetType(interfaceTypeName);
                return interfaceType != null;
            }
            
            interfaceType = script.GetClass()
                .GetInterfaces()
                .FirstOrDefault(i => i != typeof(IInjectable) && i != typeof(IDisposable));
            
            return interfaceType != null;
        }
    }
}
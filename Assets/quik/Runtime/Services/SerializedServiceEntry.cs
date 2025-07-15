using System;
using quik.Runtime.Services.Interfaces;
using UnityEditor;
using UnityEngine;
using IServiceProvider = quik.Runtime.Services.Interfaces.IServiceProvider;

namespace quik.Runtime.Services
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
            if (script == null)
            {
                return;
            }

            var type = script.GetClass();
            if (type == null)
            {
                Debug.LogWarning("[SerializedServiceEntry] Script has no class.");
                return;
            }
            
            // Create instance via Activator
            object instance = null;
            try
            {
                instance = Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Debug.LogError($"[SerializedServiceEntry] Failed to create instance of {type.Name}: {e.Message}");
                return;
            }

            if (instance == null)
            {
                Debug.LogError($"[SerializedServiceEntry] Could not instantiate {type.Name}");
                return;
            }
            
            var interfaces = type.GetInterfaces();
            Type interfaceType = null;
            
            if (!string.IsNullOrEmpty(interfaceTypeName))
            {
                interfaceType = Type.GetType(interfaceTypeName);
            }
            
            if (interfaceType == null)
            {
                foreach (var i in interfaces)
                {
                    if (i != typeof(IInjectable) && i != typeof(IDisposable))
                    {
                        interfaceType = i;
                        break;
                    }
                }
            }
            
            if (interfaceType != null)
            {
                provider.Register(interfaceType, instance);
                Debug.Log($"[SerializedServiceEntry] Registered {interfaceType.Name} from script {type.Name}");
            }
            else
            {
                Debug.LogWarning($"[SerializedServiceEntry] {type.Name} doesn't implement a usable interface.");
            }
        }
    }
}
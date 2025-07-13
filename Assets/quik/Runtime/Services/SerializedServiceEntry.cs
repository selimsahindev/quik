using System;
using quik.Runtime.Services.Interfaces;
using UnityEngine;
using IServiceProvider = quik.Runtime.Services.Interfaces.IServiceProvider;
using Object = UnityEngine.Object;

namespace quik.Runtime.Services
{
    [Serializable]
    public class SerializedServiceEntry
    {
        [Tooltip("Must implement at least one known interface.")]
        public Object serviceObject;
        
        [Header("Optional"), Tooltip("Optional override for interface type if multiple are implemented")]
        public string interfaceTypeName;
        
        /// <summary>
        /// Registers the service to the provider with interface-based registration.
        /// </summary>
        /// <param name="provider">The service provider to register the service with.</param>
        public void RegisterTo(IServiceProvider provider)
        {
            if (serviceObject == null)
            {
                return;
            }
            
            // Ensure the serviceObject implements at least one interface
            var interfaces = serviceObject.GetType().GetInterfaces();
            Type interfaceType = null;

            // If user manually specified the interface type, use that
            if (!string.IsNullOrEmpty(interfaceTypeName))
            {
                interfaceType = Type.GetType(interfaceTypeName);
            }

            // Otherwise, try to pick the first valid interface
            if (interfaceType == null)
            {
                foreach (var i in interfaces)
                {
                    if (i != typeof(IInjectable) && !typeof(UnityEngine.Object).IsAssignableFrom(i))
                    {
                        interfaceType = i;
                        break;
                    }
                }
            }

            // If an interface was found, register the service
            if (interfaceType != null)
            {
                provider.Register(interfaceType, serviceObject);
                Debug.Log($"[SerializedServiceEntry] Registered {interfaceType.Name} from {serviceObject.name}");
            }
            else
            {
                Debug.LogWarning($"[SerializedServiceEntry] {serviceObject.name} doesn't implement a usable interface.");
            }
        }
    }
}
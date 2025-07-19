using System.Collections.Generic;
using System.Linq;
using quik.Runtime.Services.Interfaces;
using UnityEngine;

namespace quik.Runtime.Services.Scriptables
{
    [CreateAssetMenu(menuName = "quik/Services/Scene Service Config", fileName = "SceneServiceConfig")]
    public class SceneServiceConfig : ScriptableObject
    {
        [Header("Scene-Specific Service Registration")]
        [SerializeField] private List<SerializedServiceEntry> services = new();

        public List<SerializedServiceEntry> Services => services;

        public void RegisterAll(IServiceProvider provider)
        {
            foreach (var entry in services.Where(entry => !IsRegisteredGlobally(entry)))
            {
                entry.RegisterTo(provider);
            }
        }

        private static bool IsRegisteredGlobally(SerializedServiceEntry serviceEntry)
        {
            return serviceEntry.TryGetInterfaceType(out var interfaceType) && ServiceLocator.Contains(interfaceType);
        }
    }
}
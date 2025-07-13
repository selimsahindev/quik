using System.Collections.Generic;
using quik.Runtime.Services.Interfaces;
using UnityEngine;

namespace quik.Runtime.Services.Scriptables
{
    [CreateAssetMenu(menuName = "quik/Scene Service Config", fileName = "SceneServiceConfig")]
    public class SceneServiceConfig : ScriptableObject
    {
        [Header("Scene-Specific Service Registration")]
        [SerializeField]
        private List<SerializedServiceEntry> services = new();

        public void RegisterAll(IServiceProvider provider)
        {
            foreach (var entry in services)
            {
                entry.RegisterTo(provider);
            }
        }
    }
}
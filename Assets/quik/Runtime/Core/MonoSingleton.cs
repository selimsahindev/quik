using UnityEngine;

namespace quik.Runtime.Core
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }
        
        /// <summary>
        /// Override this in derived class to control persistence.
        /// </summary>
        protected virtual bool PersistThroughScenes => true;

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"[MonoSingleton] Duplicate {typeof(T).Name} detected, destroying.");
                Destroy(gameObject);
                return;
            }

            Instance = (T)this;

            if (PersistThroughScenes)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
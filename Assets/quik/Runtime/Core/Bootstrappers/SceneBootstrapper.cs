using quik.Runtime.Core.Singletons;
using quik.Runtime.ServiceProvider.Enums;
using quik.Runtime.ServiceProvider.Interfaces;
using quik.Runtime.ServiceProvider.Scriptables;
using UnityEngine;
using Provider = quik.Runtime.ServiceProvider.ServiceProvider;

namespace quik.Runtime.Core.Bootstrappers
{
    public class SceneBootstrapper : MonoSingleton<SceneBootstrapper>
    {
        [SerializeField] private SceneServiceConfig config;
        
        private Provider _sceneServiceProvider;

        protected override void Awake()
        {
            base.Awake();
            Run();
        }

        private void Run()
        {
            /* 1 */ InitializeServiceProvider();
            /* 2 */ RegisterServices();
            /* 3 */ InjectSceneDependencies();
        }

        private void InitializeServiceProvider()
        {
            _sceneServiceProvider = Provider.Create(ProviderScope.Scene);
        }

        private void RegisterServices()
        {
            config.RegisterAll(_sceneServiceProvider);
        }
        
        private void InjectSceneDependencies()
        {
            foreach (var mono in FindObjectsOfType<MonoBehaviour>(true))
            {
                if (mono is ISceneInjectable sceneInjectable)
                {
                    sceneInjectable.Inject(_sceneServiceProvider);
                }
            }
        }
    }
}
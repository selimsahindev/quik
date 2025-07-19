using quik.Runtime.Services;
using quik.Runtime.Services.Interfaces;
using quik.Runtime.Services.Scriptables;
using UnityEngine;

namespace quik.Runtime.Core.Bootstrappers
{
    public class SceneBootstrapper : MonoSingleton<SceneBootstrapper>
    {
        [SerializeField] private SceneServiceConfig config;
        
        private ServiceProvider _sceneServiceProvider;

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
            _sceneServiceProvider = new ServiceProvider();
        }

        private void RegisterServices()
        {
            config.RegisterAll(_sceneServiceProvider);
        }
        
        private void InjectSceneDependencies()
        {
            foreach (var mono in FindObjectsOfType<MonoBehaviour>(true))
            {
                if (mono is not ISceneInjectable sceneInjectable)
                {
                    continue;
                }
                
                sceneInjectable.Inject(_sceneServiceProvider);
            }
        }
    }
}
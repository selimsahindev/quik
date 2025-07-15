using quik.Runtime.Services;
using quik.Runtime.Services.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace quik.Runtime.Core.Bootstrappers
{
    public class Bootstrapper : MonoSingleton<Bootstrapper>
    {
        protected override bool PersistThroughScenes => true;
        
        private ServiceProvider _provider;
        
        protected override void Awake()
        {
            InitializeServiceProvider();
            InitializeServiceLocator();
            RegisterGlobalServices();
            InjectSceneObjects();
            LoadNextScene();
        }

        private void InitializeServiceProvider()
        {
            _provider = new ServiceProvider();
            ServiceLocator.Init(_provider);
        }

        private void InitializeServiceLocator()
        {
            ServiceLocator.Init(_provider);
        }
        
        private void RegisterGlobalServices()
        {
            GlobalServiceRegistry.RegisterAll(_provider);
        }
        
        private void InjectSceneObjects()
        {
            foreach (var mono in FindObjectsOfType<MonoBehaviour>(true))
            {
                if (mono is IInjectable injectable)
                {
                    injectable.Inject(_provider);
                }
            }
        }

        private void LoadNextScene()
        {
            // TODO: Load the next scene
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InjectSceneObjects();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
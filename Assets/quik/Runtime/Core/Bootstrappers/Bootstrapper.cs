using quik.Runtime.Core.Singletons;
using quik.Runtime.Localization.Interfaces;
using quik.Runtime.SaveSystem.Constants;
using quik.Runtime.SaveSystem.Interfaces;
using quik.Runtime.SaveSystem.Models;
using quik.Runtime.ServiceProvider;
using quik.Runtime.ServiceProvider.Enums;
using quik.Runtime.ServiceProvider.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Provider = quik.Runtime.ServiceProvider.ServiceProvider;

namespace quik.Runtime.Core.Bootstrappers
{
    public class Bootstrapper : MonoSingleton<Bootstrapper>
    {
        protected override bool PersistThroughScenes => true;
        
        private Provider _provider;
        
        protected override void Awake()
        {
            base.Awake();
            Run();
        }

        private void Run()
        {
            /* 1 */ InitializeServiceProvider();
            /* 2 */ InitializeServiceLocator();
            /* 3 */ RegisterGlobalServices();
            /* 4 */ InjectSceneDependencies();
            /* 5 */ SetupLocalization();
            /* 6 */ LoadNextScene();
        }

        private void InitializeServiceProvider()
        {
            _provider = Provider.Create(ProviderScope.Global);
        }

        private void InitializeServiceLocator()
        {
            ServiceLocator.Init(_provider);
        }
        
        private void RegisterGlobalServices()
        {
            GlobalServiceRegistry.RegisterAll(_provider);
        }
        
        private void InjectSceneDependencies()
        {
            foreach (var mono in FindObjectsOfType<MonoBehaviour>(true))
            {
                if (mono is IInjectable injectable)
                {
                    injectable.Inject(_provider);
                }
            }
        }

        private void SetupLocalization()
        {
            const string errorMessage = "[Bootstrapper] Failed to resolve {0}.";
            
            if (!_provider.TryResolve(out ILocalizationManager localization))
            {
                Debug.LogError(string.Format(errorMessage, nameof(ILocalizationManager)));
            }

            if (!_provider.TryResolve(out ISaveService saveService))
            {
                Debug.LogError(string.Format(errorMessage, nameof(ISaveService)));
            }
            
            var data = saveService.Load<GameData>(Keys.GameData);
            
            localization.Load(data.settings.language);
        }

        private static void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            InjectSceneDependencies();
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
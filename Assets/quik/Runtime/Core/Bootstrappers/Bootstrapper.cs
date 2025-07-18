using quik.Runtime.Localization.Interfaces;
using quik.Runtime.SaveSystem.Constants;
using quik.Runtime.SaveSystem.Interfaces;
using quik.Runtime.SaveSystem.Models;
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
            base.Awake();
            
            /* 1 */ InitializeServiceProvider();
            /* 2 */ InitializeServiceLocator();
            /* 3 */ RegisterGlobalServices();
            /* 4 */ InjectSceneObjects();
            /* 5 */ SetupLocalization();
            /* 6 */ LoadNextScene();
        }

        private void InitializeServiceProvider()
        {
            _provider = new ServiceProvider();
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
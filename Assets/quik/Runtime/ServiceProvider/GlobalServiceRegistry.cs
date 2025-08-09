using GoogleMobileAds.Api;
using quik.Runtime.Achievements;
using quik.Runtime.Achievements.Interfaces;
using quik.Runtime.Ads.Enums;
using quik.Runtime.Ads.Interfaces;
using quik.Runtime.Ads.Services.AdMob;
using quik.Runtime.Localization.Interfaces;
using quik.Runtime.Pooling;
using quik.Runtime.Pooling.Interfaces;
using quik.Runtime.SaveSystem.Interfaces;
using quik.Runtime.SaveSystem.Services;
using quik.Runtime.Settings.Scriptables;
using quik.Runtime.Signals;
using quik.Runtime.Signals.Interfaces;
using UnityEngine;
using IServiceProvider = quik.Runtime.ServiceProvider.Interfaces.IServiceProvider;

namespace quik.Runtime.ServiceProvider
{
    /// <summary>
    /// A static registry class responsible for registering global game services
    /// into the provided service container.
    /// </summary>
    public static class GlobalServiceRegistry
    {
        /// <summary>
        /// Registers all global services required by the game into the given service provider.
        /// </summary>
        /// <param name="provider">The service provider used for dependency injection.</param>
        public static void RegisterAll(IServiceProvider provider)
        {
            // Register your global game services here using:
            // provider.Register<TInterface, TConcrete>()
            
            provider.Register<ISignalBus, SignalBus>();
            provider.Register<IPoolManager, PoolManager>();
            provider.Register<IDefaultDataService, DefaultDataService>();
            provider.Register<ISaveService, JsonSaveService>();
            provider.Register<ILocalizationManager, LocalizationManager>();
            provider.Register<IAchievementService, AchievementService>();
            
            RegisterAdService(provider);
        }

        private static void RegisterAdService(IServiceProvider provider)
        {
            switch (ProjectSettingsAsset.Instance.AdSettings.AdProvider)
            {
                case AdProvider.Admob:
                    AdMobService adMobService = new();
                    provider.Register<IBannerAdService>(adMobService);
                    provider.Register<IInterstitialAdService>(adMobService);
                    provider.Register<IRewardedAdService<Reward>>(adMobService);
                    break;
                case AdProvider.None: return;
                default:
                    Debug.Log("Selected Ad Provider is not implemented.");
                    break;
            }
        }
    }
}

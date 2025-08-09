using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Ump.Api;
using quik.Runtime.Ads.Helpers;
using quik.Runtime.Ads.Interfaces;
using quik.Runtime.Core.Extensions;
using quik.Runtime.Settings.Scriptables;
using UnityEngine;

namespace quik.Runtime.Ads.Services.AdMob
{
    public class AdMobService : IAdService, IBannerAdService, IInterstitialAdService, IRewardedAdService<Reward>
    {
        private static bool _isInitialized;
        private IInterstitialAdService _interstitialAdService;
        
        private BannerView _bannerView;
        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedAd;

        private string BannerAdUnitId => ProjectSettingsAsset.Instance.AdSettings.BannerAdUnitId;
        private string RewardedAdUnitId => ProjectSettingsAsset.Instance.AdSettings.RewardedAdUnitId;
        private string InterstitialAdUnitId => ProjectSettingsAsset.Instance.AdSettings.InterstitialAdUnitId;
        
        public void Initialize()
        {
            // On Android, Unity is paused when displaying interstitial or rewarded video.
            // This setting makes iOS behave consistently with Android.
            MobileAds.SetiOSAppPauseOnBackground(true);

            // When true all events raised by GoogleMobileAds will be raised
            // on the Unity main thread. The default value is false.
            // https://developers.google.com/admob/unity/quick-start#raise_ad_events_on_the_unity_main_thread
            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            
            MobileAds.SetRequestConfiguration(new RequestConfiguration
            {
                TagForChildDirectedTreatment = TagForChildDirectedTreatment.False,
            });
            
            // If we can request ads, we should initialize the Google Mobile Ads Unity plugin.
            if (GoogleMobileAdsConsentHelper.CanRequestAds)
            {
                InitializeGoogleMobileAds(_interstitialAdService.LoadInterstitial);
            }
            
            GoogleMobileAdsConsentHelper.GatherConsent(HandleGatherConsentCompleted);
        }

        public void LoadBanner()
        {
            if (_bannerView != null)
            {
                _bannerView.Destroy();
            }
            
            _bannerView = new BannerView(BannerAdUnitId, AdSize.Banner, AdPosition.Bottom);
            _bannerView.OnBannerAdLoaded += () => Debug.Log("Banner loaded.");
            _bannerView.OnBannerAdLoadFailed += (error) => Debug.LogError($"Banner loading failed: {error.GetMessage()}");
            _bannerView.LoadAd(CreateAdRequest());
        }

        public void ShowBanner()
        {
            _bannerView?.Show();
        }

        public void HideBanner()
        {
            _bannerView?.Hide();
        }

        public void LoadRewarded()
        {
            if (_rewardedAd != null)
            {
                _interstitialAd.Destroy();
            }
            
            RewardedAd.Load(RewardedAdUnitId, CreateAdRequest(), (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogWarning($"Rewarded loading failed: {error?.GetMessage()}");
                    return;
                }

                _rewardedAd = ad;
                RegisterReloadHandler(_rewardedAd);
            });
        }

        public void ShowRewarded(Action<Reward> userRewardEarnedCallback)
        {
            _rewardedAd.Show(userRewardEarnedCallback);
        }

        public void LoadInterstitial()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
            }

            InterstitialAd.Load(InterstitialAdUnitId, CreateAdRequest(), (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogWarning($"Interstitial ad loading failed: {error?.GetMessage()}");
                    return;
                }

                _interstitialAd = ad;
                RegisterReloadHandler(_interstitialAd);
            });
        }

        public void ShowInterstitial()
        {
            _interstitialAd?.Show();
        }
        
        /// <summary>
        /// Ensures that privacy and consent information is up to date.
        /// </summary>
        private void HandleGatherConsentCompleted(string error)
        {
            string message = error.IsNullOrEmpty() ?
                $"Google Mobile Ads consent updated: {ConsentInformation.ConsentStatus}" :
                $"Consent failed for Google Mobile Ads: {error}";

            if (GoogleMobileAdsConsentHelper.CanRequestAds)
            {
                InitializeGoogleMobileAds();
            }
        }
        
        private static void InitializeGoogleMobileAds(Action onInitialized = null)
        {
            if (_isInitialized)
            {
                return;
            }

            MobileAds.Initialize(initStatus =>
            {
                if (initStatus == null)
                {
                    Debug.LogError("Google Mobile Ads initialization failed.");
                    return;
                }
                
                // If using mediation, we can check the status of each adapter.
                var adapterStatusMap = initStatus.getAdapterStatusMap();
                if (adapterStatusMap != null)
                {
                    foreach (var item in adapterStatusMap)
                    {
                        Debug.Log($"Adapter {item.Key} is {item.Value.InitializationState}");
                    }
                }
                
                Debug.Log("Google Mobile Ads initialization complete.");
                
                _isInitialized = true;
                onInitialized?.Invoke();
            });
        }
        
        private void RegisterReloadHandler(InterstitialAd interstitialAd)
        {
            interstitialAd.OnAdFullScreenContentClosed += LoadInterstitial;
            interstitialAd.OnAdFullScreenContentFailed += error =>
            {
                Debug.LogError($"Interstitial ad failed to open full screen content with error: {error}");
                LoadInterstitial();
            };
        }
        
        private void RegisterReloadHandler(RewardedAd rewardedAd)
        {
            rewardedAd.OnAdFullScreenContentClosed += LoadInterstitial;
            rewardedAd.OnAdFullScreenContentFailed += error =>
            {
                Debug.LogError($"Interstitial ad failed to open full screen content with error: {error}");
                LoadInterstitial();
            };
        }
        
        private static AdRequest CreateAdRequest()
        {
            return new AdRequest();
        }
    }
}
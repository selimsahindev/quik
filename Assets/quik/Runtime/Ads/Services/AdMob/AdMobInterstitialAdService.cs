using GoogleMobileAds.Api;
using quik.Runtime.Ads.Interfaces;
using UnityEngine;

namespace quik.Runtime.Ads.Services.AdMob
{
    public class AdMobInterstitialAdService : IInterstitialAdService
    {
        private const string InterstitialAdUnitId = "";
        private InterstitialAd _interstitialAd;
        
        public void LoadInterstitial()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }
            
            InterstitialAd.Load(InterstitialAdUnitId, new AdRequest(), (ad, error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError($"interstitial ad failed to load an ad with error: {error}");
                    return;
                }
                _interstitialAd = ad;
                RegisterReloadHandler(_interstitialAd);
                Debug.Log($"Interstitial ad loaded with response: {ad.GetResponseInfo()}");
            });
        }

        public void ShowInterstitial()
        {
            if (_interstitialAd == null || !_interstitialAd.CanShowAd())
            {
                Debug.LogWarning("Could not show interstitial ad. Trying to load a new one.");
                LoadInterstitial();
                return;
            }
            
            _interstitialAd.Show();
        }
        
        private void RegisterReloadHandler(InterstitialAd interstitialAd)
        {
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                Debug.Log("Interstitial Ad full screen content closed.");
                LoadInterstitial();
            };
            interstitialAd.OnAdFullScreenContentFailed += error =>
            {
                Debug.LogError($"Interstitial ad failed to open full screen content with error: {error}");
                LoadInterstitial();
            };
        }
    }
}
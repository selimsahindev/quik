using quik.Runtime.Ads.Enums;
using UnityEngine;

namespace quik.Runtime.Settings.Scriptables
{
    [CreateAssetMenu(menuName = "quik/Settings/Ad Settings", fileName = "NewAdSettings")]
    public class AdSettings : ScriptableObject
    {
        [SerializeField] private AdProvider adProvider;
        
        [Header("Ad Unit Ids"), Space(10)]
        [SerializeField] private string bannerAdUnitId;
        [SerializeField] private string interstitialAdUnitId;
        [SerializeField] private string rewardedAdUnitId;
        
        public AdProvider AdProvider => adProvider;
        public string BannerAdUnitId => bannerAdUnitId;
        public string InterstitialAdUnitId => interstitialAdUnitId;
        public string RewardedAdUnitId => rewardedAdUnitId;
    }
}
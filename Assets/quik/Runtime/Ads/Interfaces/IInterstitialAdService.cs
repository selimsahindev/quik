namespace quik.Runtime.Ads.Interfaces
{
    public interface IInterstitialAdService
    {
        /// <summary>
        /// Loads the interstitial ad in the background.
        /// </summary>
        void LoadInterstitial();

        /// <summary>
        /// Shows the interstitial ad if it's loaded and ready.
        /// </summary>
        void ShowInterstitial();
    }
}
namespace quik.Runtime.Ads.Interfaces
{
    public interface IBannerAdService
    {
        /// <summary>
        /// Loads and prepares the banner ad.
        /// </summary>
        void LoadBanner();

        /// <summary>
        /// Displays the banner ad if it's loaded.
        /// </summary>
        void ShowBanner();

        /// <summary>
        /// Hides the currently shown banner ad.
        /// </summary>
        void HideBanner();
    }
}
using System;
using GoogleMobileAds.Ump.Api;
using UnityEngine;

namespace quik.Runtime.Ads.Helpers
{
    /// <summary>
    /// Helper class that implements consent using the Google User Messaging Platform (UMP) SDK.
    /// </summary>
    public class GoogleMobileAdsConsentHelper : MonoBehaviour
    {
        /// <summary>
        /// If true, it is safe to call MobileAds.Initialize() and load Ads.
        /// </summary>
        public static bool CanRequestAds => ConsentInformation.CanRequestAds();

        /// <summary>
        /// Startup method for the Google User Messaging Platform (UMP) SDK
        /// which will run all startup logic including loading any required
        /// updates and displaying any required forms.
        /// </summary>
        public static void GatherConsent(Action<string> onComplete)
        {
            var requestParameters = new ConsentRequestParameters
            {
                // False means users are not under age.
                TagForUnderAgeOfConsent = false,
                ConsentDebugSettings = new ConsentDebugSettings
                {
                    // For debugging consent settings by geography.
                    DebugGeography = DebugGeography.Disabled,
                }
            };

            // The Google Mobile Ads SDK provides the User Messaging Platform (Google's
            // IAB Certified consent management platform) as one solution to capture
            // consent for users in GDPR impacted countries. This is an example and
            // you can choose another consent management platform to capture consent.
            ConsentInformation.Update(requestParameters, updateError =>
            {
                if (updateError != null)
                {
                    onComplete(updateError.Message);
                    return;
                }

                // Determine the consent-related action to take based on the ConsentStatus.
                if (CanRequestAds)
                {
                    // Consent has already been gathered or not required.
                    // Return control back to the user.
                    onComplete(null);
                    return;
                }

                // Consent not obtained and is required.
                // Load the initial consent request form for the user.
                // Form showing succeeded.
                // Form showing failed.
                ConsentForm.LoadAndShowConsentFormIfRequired(showError =>
                {
                    onComplete?.Invoke(showError?.Message);
                });
            });
        }
    }
}

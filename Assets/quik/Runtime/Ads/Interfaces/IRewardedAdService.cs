using System;

namespace quik.Runtime.Ads.Interfaces
{
    public interface IRewardedAdService<TReward>
    {
        /// <summary>
        /// Loads the rewarded ad in the background.
        /// </summary>
        void LoadRewarded();

        /// <summary>
        /// Shows the rewarded ad and invokes the callback if the user earns the reward.
        /// </summary>
        /// <param name="userRewardEarnedCallback">Callback invoked after the user earns the reward.</param>
        void ShowRewarded(Action<TReward> userRewardEarnedCallback);
    }
}

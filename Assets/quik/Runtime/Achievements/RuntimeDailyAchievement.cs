using quik.Runtime.Achievements.Scriptables;
using IAchievement = quik.Runtime.Achievements.Interfaces.IAchievement;

namespace quik.Runtime.Achievements
{
    public class RuntimeDailyAchievement : IAchievement
    {
        public string Id => _asset.Id;

        private readonly DailyAchievementAsset _asset;
        private string _rewardGranted1;

        public RuntimeDailyAchievement(DailyAchievementAsset asset)
        {
            _asset = asset;
            _asset.Condition.onConditionMet += OnConditionMet;
        }

        private void OnConditionMet()
        {
            CheckProgress();
        }

        public void CheckProgress()
        {
            if (_asset.Condition.IsCompleted())
            {
                GrantReward();
                ResetProgress();
            }
        }
        
        public void GrantReward()
        {
            _asset.Reward.Grant();
            // TODO: Persist state!
        }

        public void ResetProgress()
        {
            _asset.Condition.ResetProgress();
        }
    }
}
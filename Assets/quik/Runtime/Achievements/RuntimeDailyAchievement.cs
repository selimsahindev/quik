using quik.Runtime.Achievements.Scriptables;
using IAchievement = quik.Runtime.Achievements.Interfaces.IAchievement;

namespace quik.Runtime.Achievements
{
    public class RuntimeDailyAchievement : IAchievement
    {
        private readonly DailyAchievementAsset _asset;

        public string Id => _asset.Id;
        public bool IsCompleted => _asset.Condition.CheckCondition();
                
        public RuntimeDailyAchievement(DailyAchievementAsset asset)
        {
            _asset = asset;
        }
        
        public void CheckProgress()
        {
            if (IsCompleted)
            {
                GrantReward();
            }
        }

        public void GrantReward()
        {
            _asset.Reward.Grant();
            // TODO: Persist state
        }

        public void ResetProgress()
        {
            _asset.Condition.ResetProgress();
        }
    }
}
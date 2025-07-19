using quik.Runtime.Achievements.Interfaces;
using UnityEngine;

namespace quik.Runtime.Achievements.Scriptables
{
    [CreateAssetMenu(menuName = "quik/Achievements/Daily Achievement", fileName = "NewDailyAchievement")]
    public class DailyAchievementAsset : ScriptableObject
    {
        public string Id;
        public string Description;
        public RewardAsset Reward;
        public IAchievementCondition Condition;
    }
}

using System;
using UnityEngine;

namespace quik.Runtime.Achievements.Scriptables
{
    [CreateAssetMenu(menuName = "quik/Achievements/Reward", fileName = "NewReward")]
    public class RewardAsset : ScriptableObject
    {
        public int coins;
        public int gems;
        
        public void Grant()
        {
            // Handle reward earning logic, for example:
            // PlayerInventory.Add(coins, gems);
            throw new NotImplementedException();
        }
    }
}
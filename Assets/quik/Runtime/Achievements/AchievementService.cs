using System.Collections.Generic;
using quik.Runtime.Achievements.Interfaces;
using quik.Runtime.Achievements.Scriptables;
using quik.Runtime.ServiceProvider;
using quik.Runtime.ServiceProvider.Interfaces;
using quik.Runtime.Signals.Interfaces;
using UnityEngine;

namespace quik.Runtime.Achievements
{
    public class AchievementService : IAchievementService
    {
        public List<IAchievement> Achievements { get; } = new();
        private readonly ISignalBus _signalBus;

        public AchievementService()
        {
            _signalBus = ServiceLocator.Resolve<ISignalBus>();
            
            Initialize();
        }
        
        public void Initialize()
        {
            SubscribeConditionsToSignals(LoadDailyAchievements());
        }

        public void ResetAll()
        {
            foreach (var achievement in Achievements)
            {
                achievement.ResetProgress();
            }
        }

        private void SubscribeConditionsToSignals(DailyAchievementAsset[] assets)
        {
            if (_signalBus == null)
            {
                Debug.LogWarning($"[{nameof(AchievementService)}] Binding conditions to signals failed — signal bus is null.");
                return;
            }
            
            foreach (var asset in assets)
            {
                var runtime = new RuntimeDailyAchievement(asset);
                
                runtime.CheckProgress();
                Achievements.Add(runtime);
                asset.Condition.StartListening(_signalBus);
            }
        }

        private static DailyAchievementAsset[] LoadDailyAchievements()
        {
            return Resources.LoadAll<DailyAchievementAsset>("Achievements/Daily");
        }
    }
}

using System.Collections.Generic;
using quik.Runtime.Achievements.Interfaces;
using quik.Runtime.Achievements.Scriptables;
using quik.Runtime.Services.Interfaces;
using quik.Runtime.Signals.Interfaces;
using UnityEngine;

namespace quik.Runtime.Achievements
{
    public class AchievementService : IAchievementService, IInjectable
    {
        public List<IAchievement> Achievements { get; } = new();
        
        private ISignalBus _signalBus;

        public void Inject(IServiceProvider provider)
        {
            _signalBus = provider.Resolve<ISignalBus>();
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
                return;
            }
            
            foreach (var asset in assets)
            {
                var runtime = new RuntimeDailyAchievement(asset);
                
                runtime.CheckProgress();
                Achievements.Add(runtime);
                asset.Condition.SubscribeToSignal(_signalBus);
            }
        }

        private static DailyAchievementAsset[] LoadDailyAchievements()
        {
            return Resources.LoadAll<DailyAchievementAsset>("Achievements/Daily");
        }
        
        private static void LogSignalBusResolutionError()
        {
            Debug.LogError($"[{nameof(AchievementService)}] Failed to " +
                           "resolve dependency: ISignalBus. Achievement system will not function properly.");
        }
    }
}

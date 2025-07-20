using quik.Runtime.Signals.Gameplay;
using quik.Runtime.Signals.Interfaces;
using UnityEngine;

namespace quik.Runtime.Achievements.Scriptables.Conditions
{
    [CreateAssetMenu(menuName = "quik/Achievements/Conditions/Kill Enemies Condition", fileName = "KillEnemiesCondition")]
    public class KillEnemiesCondition : ConditionBase
    {
        [SerializeField] private int requiredKills = 10;
        [SerializeField] private int currentKills;
        
        public override void StartListening(ISignalBus signalBus)
        {
            signalBus.Subscribe<EnemyKilledSignal>(HandleEnemyKilled);
        }
        
        public override bool IsCompleted() => currentKills >= requiredKills;

        public override void ResetProgress() => currentKills = 0;

        public override void TriggerConditionMet()
        {
            onConditionMet?.Invoke();
            Debug.Log($"[{nameof(KillEnemiesCondition)}] Condition met!");
        }

        private void HandleEnemyKilled(EnemyKilledSignal signal)
        {
            if (IsCompleted())
            {
                return;
            }
            
            currentKills++;
            
            if (IsCompleted())
            {
                TriggerConditionMet();
            }
        }
    }
}
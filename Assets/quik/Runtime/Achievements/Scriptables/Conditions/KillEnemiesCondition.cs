using quik.Runtime.Achievements.Interfaces;
using quik.Runtime.Signals.Gameplay;
using quik.Runtime.Signals.Interfaces;
using UnityEngine;

namespace quik.Runtime.Achievements.Scriptables.Conditions
{
    public class KillEnemiesCondition : ScriptableObject, IAchievementCondition
    {
        public int requiredKills = 10;
        private int _currentKills;
        
        public void SubscribeToSignal(ISignalBus signalBus)
        {
            signalBus.Subscribe<EnemyKilledSignal>(OnSignalReceived);
        }

        public bool CheckCondition() => _currentKills >= requiredKills;

        public void ResetProgress() => _currentKills = 0;

        private void OnSignalReceived(EnemyKilledSignal signal)
        {
            _currentKills++;
            Debug.Log($"[{nameof(KillEnemiesCondition)}] fired. Current kills: {_currentKills}");
        }
    }
}
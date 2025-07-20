using System;
using quik.Runtime.Signals.Interfaces;
using UnityEngine;

namespace quik.Runtime.Achievements.Scriptables.Conditions
{
    public abstract class ConditionBase : ScriptableObject
    {
        public abstract void StartListening(ISignalBus signalBus);
        public abstract bool IsCompleted();
        public abstract void ResetProgress();
        public abstract void TriggerConditionMet();
        public Action onConditionMet;
    }
}
using quik.Runtime.Signals.Interfaces;

namespace quik.Runtime.Achievements.Interfaces
{
    public interface IAchievementCondition
    {
        public void SubscribeToSignal(ISignalBus signalBus);
        public bool CheckCondition();
        public void ResetProgress();
    }
}
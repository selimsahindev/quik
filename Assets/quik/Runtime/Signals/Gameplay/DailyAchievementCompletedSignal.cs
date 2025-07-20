using quik.Runtime.Signals.Interfaces;

namespace quik.Runtime.Signals.Gameplay
{
    public class DailyAchievementCompletedSignal : ISignal
    {
        public string Id;
        public string Description;
    }
}

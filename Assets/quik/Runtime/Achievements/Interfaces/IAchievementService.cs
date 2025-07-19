using System.Collections.Generic;

namespace quik.Runtime.Achievements.Interfaces
{
    public interface IAchievementService
    {
        List<IAchievement> Achievements { get; }
        void Initialize();
        void ResetAll();
    }
}
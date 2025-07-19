namespace quik.Runtime.Achievements.Interfaces
{
    public interface IAchievement
    {
        string Id { get; }
        bool IsCompleted { get; }
        void CheckProgress();
        void GrantReward();
        void ResetProgress();
    }
}

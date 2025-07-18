using System;
using quik.Runtime.Core.Interfaces;

namespace quik.Runtime.SaveSystem.Models
{
    [Serializable]
    public class PlayerProgressData : ICloneable<PlayerProgressData>
    {
        public int currentLevel = 1;
        public int highScore;
        public bool tutorialCompleted;
        
        public PlayerProgressData Clone()
        {
            return new PlayerProgressData
            {
                currentLevel = currentLevel,
                highScore = highScore,
                tutorialCompleted = tutorialCompleted
            };
        }
    }
}
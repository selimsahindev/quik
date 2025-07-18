using System;
using quik.Runtime.Core.Interfaces;

namespace quik.Runtime.SaveSystem.Models
{
    [Serializable]
    public class SettingsData : ICloneable<SettingsData>
    {
        public bool soundOn = true;
        public bool musicOn = true;
        public bool vibrationEnabled = true;
        public string language = "en-EN";
        
        public SettingsData Clone()
        {
            return new SettingsData
            {
                soundOn = soundOn,
                musicOn = musicOn,
                vibrationEnabled = vibrationEnabled,
                language = language
            };
        }
    }
}
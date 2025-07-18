using System;
using quik.Runtime.Core.Interfaces;

namespace quik.Runtime.SaveSystem.Models
{
    [Serializable]
    public class MetaData : ICloneable<MetaData>
    {
        public int saveVersion = 1;
        public string lastPlayedDate;
        
        public MetaData Clone()
        {
            return new MetaData
            {
                saveVersion = saveVersion,
                lastPlayedDate = lastPlayedDate
            };
        }
    }
}
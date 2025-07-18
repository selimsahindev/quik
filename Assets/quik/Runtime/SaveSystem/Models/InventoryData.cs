using System;
using System.Collections.Generic;
using quik.Runtime.Core.Interfaces;

namespace quik.Runtime.SaveSystem.Models
{
    [Serializable]
    public class InventoryData : ICloneable<InventoryData>
    {
        public List<string> unlockedSkins = new();
        public string equippedSkin = "default";
        
        public InventoryData Clone()
        {
            return new InventoryData
            {
                unlockedSkins = new List<string>(unlockedSkins),
                equippedSkin = equippedSkin
            };
        }
    }
}
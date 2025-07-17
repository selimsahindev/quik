using System.Collections.Generic;

namespace quik.Runtime.SaveSystem.Models
{
    [System.Serializable]
    public class InventoryData
    {
        public List<string> unlockedSkins = new();
        public string equippedSkin = "default";
    }
}
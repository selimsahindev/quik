using System;
using quik.Runtime.Core.Interfaces;

namespace quik.Runtime.SaveSystem.Models
{
    [Serializable]
    public class GameData : ICloneable<GameData>
    {
        public MetaData meta = new();
        public PlayerProgressData progress = new();
        public SettingsData settings = new();
        public CurrencyData currency = new();
        public InventoryData inventory = new();
        
        public GameData Clone()
        {
            return new GameData
            {
                meta = meta.Clone(),
                progress = progress.Clone(),
                settings = settings.Clone(),
                currency = currency.Clone(),
                inventory = inventory.Clone()
            };
        }
    }
}
namespace quik.Runtime.SaveSystem.Models
{
    [System.Serializable]
    public class GameData
    {
        public MetaData meta = new();
        public PlayerProgressData progress = new();
        public SettingsData settings = new();
        public CurrencyData currency = new();
        public InventoryData inventory = new();
    }
}
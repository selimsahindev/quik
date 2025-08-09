namespace quik.Runtime.UI.PanelSystem.Enums
{
    public enum PanelKey
    {
        MainMenu,
        Settings
    }

    public static class PanelKeyExtensions
    {
        public static string GetKey(this PanelKey key) => key.ToString();
    }
}

namespace quik.Runtime.UI.PanelSystem.Signals
{
    /// <summary>
    /// Fired when the panel navigation should go back to the previous panel.
    /// </summary>
    public class BackToPreviousPanelSignal : PanelSignalBase
    {
        public BackToPreviousPanelSignal(string panelKey) : base(panelKey) { }
    }
}
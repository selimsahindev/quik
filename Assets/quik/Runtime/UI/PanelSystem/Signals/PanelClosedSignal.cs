namespace quik.Runtime.UI.PanelSystem.Signals
{
    /// <summary>
    /// Fired when a specific panel is closed.
    /// </summary>
    public class PanelClosedSignal : PanelSignalBase
    {
        public PanelClosedSignal(string panelKey) : base(panelKey) { }
    }
}

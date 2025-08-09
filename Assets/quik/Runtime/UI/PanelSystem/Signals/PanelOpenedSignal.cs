namespace quik.Runtime.UI.PanelSystem.Signals
{
    /// <summary>
    /// Fired when a specific panel is opened.
    /// </summary>
    public class PanelOpenedSignal : PanelSignalBase
    {
        public PanelOpenedSignal(string panelKey) : base(panelKey) { }
    }
}

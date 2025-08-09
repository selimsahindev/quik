using quik.Runtime.Signals.Interfaces;

namespace quik.Runtime.UI.PanelSystem.Signals
{
    /// <summary>
    /// Base class for panel-related signals, carrying the unique key identifying the target panel.
    /// Used as a common foundation for signals that operate on a specific panel.
    /// </summary>
    public abstract class PanelSignalBase : ISignal
    {
        /// <summary>
        /// The unique key/name of the panel to close.
        /// </summary>
        public string PanelKey { get; }

        protected PanelSignalBase(string panelKey)
        {
            PanelKey = panelKey;
        }
    }
}

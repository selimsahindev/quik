using System.Collections.Generic;
using quik.Runtime.Core.Singletons;
using quik.Runtime.ServiceProvider.Interfaces;
using quik.Runtime.Signals.Interfaces;
using quik.Runtime.UI.PanelSystem.Panels;
using quik.Runtime.UI.PanelSystem.Signals;

namespace quik.Runtime.UI.PanelSystem
{
    /// <summary>
    /// Manages UI panels by opening, closing, and navigating back through panel history.
    /// Publishes signals to notify other components about panel state changes.
    /// </summary>
    public class PanelManager : MonoSingleton<PanelManager>, IInjectable
    {
        private ISignalBus _signalBus;
        private readonly Stack<string> _history = new();
        private readonly Dictionary<string, BasePanel> _panels = new();

        public void Inject(IServiceProvider provider)
        {
            _signalBus = provider.Resolve<ISignalBus>();
        }

        /// <summary>
        /// Registers a panel instance with a unique key for management.
        /// </summary>
        /// <param name="key">Unique identifier for the panel.</param>
        /// <param name="panel">Panel instance to register.</param>
        public void RegisterPanel(string key, BasePanel panel)
        {
            _panels.TryAdd(key, panel);
        }

        /// <summary>
        /// Opens the panel identified by the given key, hides the current panel,
        /// updates navigation history, and fires a PanelOpenedSignal.
        /// </summary>
        /// <param name="panelKey">Key of the panel to open.</param>
        public void OpenPanel(string panelKey)
        {
            if (_panels.TryGetValue(panelKey, out var panel))
            {
                if (_history.Count > 0)
                {
                    var current = _panels[_history.Peek()];
                    current.Hide();
                }

                panel.Show();
                _history.Push(panelKey);

                // Notify others that a panel was opened
                _signalBus?.Fire(new PanelOpenedSignal(panelKey));
            }
        }
        
        /// <summary>
        /// Closes the panel identified by the given key and fires a PanelClosedSignal.
        /// </summary>
        /// <param name="panelKey">Key of the panel to close.</param>
        public void ClosePanel(string panelKey)
        {
            if (_panels.TryGetValue(panelKey, out var panel))
            {
                panel.Hide();
                // _history.Remove(panelKey); // if you want to handle this

                // Notify others that a panel was closed
                _signalBus?.Fire(new PanelClosedSignal(panelKey));
            }
        }
        
        /// <summary>
        /// Navigates back to the previous panel in history, hides the current panel,
        /// shows the previous one, and fires a BackToPreviousPanelSignal.
        /// </summary>
        public void BackToPreviousPanel()
        {
            if (_history.Count <= 1)
            {
                return;
            }
            
            var currentKey = _history.Pop();
            var current = _panels[currentKey];
            current.Hide();

            var previousKey = _history.Peek();
            var previous = _panels[previousKey];
            previous.Show();

            // Notify others that navigation back occurred
            _signalBus?.Fire(new BackToPreviousPanelSignal(previousKey));
        }
    }
}

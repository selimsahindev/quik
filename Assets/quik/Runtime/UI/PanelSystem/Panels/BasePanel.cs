using quik.Runtime.UI.PanelSystem.Interfaces;
using UnityEngine;

namespace quik.Runtime.UI.PanelSystem.Panels
{
    /// <summary>
    /// Serves as the abstract base class for all UI panels in the project.
    /// Provides default behavior for showing and hiding panels, and integrates
    /// with a customizable transition system via <see cref="IPanelTransition"/>.
    /// </summary>
    public abstract class BasePanel : MonoBehaviour
    {
        /// <summary>
        /// The transition handler for this panel. Controls show/hide animations.
        /// </summary>
        protected IPanelTransition Transition;
        
        /// <summary>
        /// Called automatically when the panel has finished showing.
        /// Override to add setup logic for derived panels.
        /// </summary>
        protected virtual void OnShown() { }
        
        /// <summary>
        /// Called automatically when the panel has finished hiding.
        /// Deactivates the GameObject by default.
        /// </summary>
        protected virtual void OnHiden() => gameObject.SetActive(false);
        
        /// <summary>
        /// Initializes the panel with a specific transition handler.
        /// This should be called before the panel is shown for the first time.
        /// </summary>
        /// <param name="transition">The transition implementation to use.</param>
        public virtual void Initialize(IPanelTransition transition)
        {
            Transition = transition;
        }

        /// <summary>
        /// Displays the panel and plays its show transition.
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
            Transition?.PlayShow(OnShown);
        }
        
        /// <summary>
        /// Hides the panel and plays its hide transition.
        /// </summary>
        public virtual void Hide()
        {
            Transition?.PlayHide(OnHiden);
        }
    }
}

using DG.Tweening;

namespace quik.Runtime.UI.PanelSystem.Interfaces
{
    /// <summary>
    /// Defines a contract for playing show/hide animations on UI panels.
    /// Implement this interface to create custom transition effects
    /// (e.g., fade, slide, scale) that can be plugged into the panel system
    /// without modifying panel logic.
    /// </summary>
    public interface IPanelTransition
    {
        /// <summary>
        /// Plays the show animation for a panel.
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the animation completes.</param>
        void PlayShow(TweenCallback onComplete = null);
        
        /// <summary>
        /// Plays the hide animation for a panel.
        /// </summary>
        /// <param name="onComplete">Optional callback invoked when the animation completes.</param>
        void PlayHide(TweenCallback onComplete = null);
    }
}

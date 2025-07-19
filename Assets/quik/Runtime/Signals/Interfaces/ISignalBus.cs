using System;

namespace quik.Runtime.Signals.Interfaces
{
    /// <summary>
    /// Represents a signal bus system for publishing and subscribing to typed signals.
    /// </summary>
    public interface ISignalBus
    {
        /// <summary>
        /// Subscribes a callback to a signal of type <typeparamref name="T"/>.
        /// The callback will be invoked when the signal is fired.
        /// </summary>
        /// <typeparam name="T">The type of signal to subscribe to.</typeparam>
        /// <param name="callback">The method to invoke when the signal is fired.</param>
        void Subscribe<T>(Action<T> callback) where T : ISignal;

        /// <summary>
        /// Unsubscribes a previously subscribed callback from the signal of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of signal to unsubscribe from.</typeparam>
        /// <param name="callback">The callback method to remove.</param>
        void Unsubscribe<T>(Action<T> callback) where T : ISignal;

        /// <summary>
        /// Fires a signal of type <typeparamref name="T"/>, notifying all subscribed listeners.
        /// </summary>
        /// <typeparam name="T">The type of signal being fired.</typeparam>
        /// <param name="signal">The signal instance to dispatch.</param>
        void Fire<T>(T signal) where T : ISignal;

        /// <summary>
        /// Clears all signal subscriptions from the bus.
        /// </summary>
        void Clear();
    }
}
using System;
using System.Collections.Generic;
using quik.Runtime.Signals.Interfaces;

namespace quik.Runtime.Signals
{
    public class SignalBus : ISignalBus
    {
        private readonly Dictionary<Type, List<Delegate>> _listeners;

        public SignalBus()
        {
            _listeners = new Dictionary<Type, List<Delegate>>();
        }

        public void Subscribe<T>(Action<T> callback) where T : ISignal
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
            {
                _listeners[type] = new List<Delegate>();
            }
            _listeners[type].Add(callback);
        }

        public void Unsubscribe<T>(Action<T> callback) where T : ISignal
        {
            var type = typeof(T);
            if (_listeners.TryGetValue(type, out var listener))
            {
                listener.Remove(callback);
            }
        }

        public void Fire<T>(T signal) where T : ISignal
        {
            var type = typeof(T);
            if (!_listeners.ContainsKey(type))
            {
                return;
            }
            foreach (var callback in _listeners[type])
            {
                ((Action<T>)callback)?.Invoke(signal);
            }
        }

        public void Clear()
        {
            _listeners.Clear();
        }
    }    
}

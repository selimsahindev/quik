using System;
using System.Collections.Generic;
using quik.Runtime.Signals.Interfaces;

namespace quik.Runtime.Signals
{
    public class SignalBus : ISignalBus
    {
        private readonly Dictionary<Type, List<Delegate>> _listeners = new();

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
            
            // Iterate over a copy to avoid: "Collection was modified; enumeration operation may not execute." errors.
            var listenersCopy = new List<Delegate>(_listeners[type]);
            foreach (var callback in listenersCopy)
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

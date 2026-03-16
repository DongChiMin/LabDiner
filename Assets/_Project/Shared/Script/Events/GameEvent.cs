using System.Collections.Generic;
using UnityEngine;

namespace LabDiner.Shared.Events
{
    public abstract class GameEvent<T> : ScriptableObject
    {
        private readonly List<System.Action<T>> _listeners = new List<System.Action<T>>();

        public void Raise(T data)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].Invoke(data);
        }

        public void Register(System.Action<T> listener) => _listeners.Add(listener);
        public void Unregister(System.Action<T> listener) => _listeners.Remove(listener);
    }
}
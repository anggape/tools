using System.Collections.Generic;

namespace Ape.Scriptable
{
    public abstract class EventBase<T> : ScriptableBase
    {
        protected List<T> _listeners = new List<T>();

        public void Subscribe(T listener) => _listeners.Add(listener);

        public void Unsubscribe(T listener) => _listeners.Remove(listener);
    }
}

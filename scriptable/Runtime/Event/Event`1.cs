namespace Ape.Scriptable
{
    public abstract class Event<T> : EventBase<EventListener<T>>
    {
        public void Invoke(T value) => _listeners.ForEach(x => x?.Invoke(value));
    }
}

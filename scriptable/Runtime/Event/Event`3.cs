namespace Ape.Scriptable
{
    public abstract class Event<T1, T2, T3> : EventBase<EventListener<T1, T2, T3>>
    {
        public void Invoke(T1 value1, T2 value2, T3 value3) =>
            _listeners.ForEach(x => x?.Invoke(value1, value2, value3));
    }
}

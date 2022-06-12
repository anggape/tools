namespace Ape.Scriptable
{
    public class Event : EventBase<EventListener>
    {
        public void Invoke() => _listeners.ForEach(x => x?.Invoke());
    }
}

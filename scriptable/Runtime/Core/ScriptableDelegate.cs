namespace Ape.Scriptable
{
    public delegate void EventListener();
    public delegate void EventListener<T>(T value);
    public delegate void EventListener<T1, T2>(T1 value1, T2 value2);
    public delegate void EventListener<T1, T2, T3>(T1 value1, T2 value2, T3 value3);
}

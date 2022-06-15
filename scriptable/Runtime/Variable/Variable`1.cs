using UnityEngine;

namespace Ape.Scriptable
{
    public abstract class Variable<T> : ScriptableBase, ISerializationCallbackReceiver
    {
        [SerializeField]
        private T _default;

        private T _value;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() { }

        void ISerializationCallbackReceiver.OnBeforeSerialize() => _value = _default;
    }
}

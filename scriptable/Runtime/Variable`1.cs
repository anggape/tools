using UnityEngine;

namespace Ape.Scriptable
{
    public abstract class Variable<T> : ScriptableBase
    {
        [SerializeField]
        private T _default;

        private T _value;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        private void OnEnable() => _value = _default;
    }
}

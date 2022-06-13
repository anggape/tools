using System.Collections.Generic;
using UnityEngine;

namespace Ape.Scriptable
{
    [CreateAssetMenu]
    public class ScriptableGroup : ScriptableObject
    {
        [SerializeField]
        private List<ScriptableBase> _scriptables = new List<ScriptableBase>();
    }
}

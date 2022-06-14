using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ape.Scriptable
{
    internal class ScriptableDatabase : ScriptableObject
    {
        [Serializable]
        internal class Category
        {
            [SerializeField]
            private string _name;

            [SerializeField]
            private List<ScriptableBase> _scriptables = new List<ScriptableBase>();

            public string Name => _name;
            public ScriptableBase[] Scriptables => _scriptables.ToArray();
        }

        [SerializeField]
        private List<Category> _categories = new List<Category>();

        public Category[] Categories => _categories.ToArray();
    }
}

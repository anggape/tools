using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ape.Scriptable
{
    public class ScriptableDatabase : ScriptableObject
    {
        [Serializable]
        private class Category
        {
            [SerializeField]
            private string _name;

            [SerializeField]
            private List<ScriptableBase> _scriptables = new List<ScriptableBase>();
        }

        [SerializeField]
        private List<Category> _categories = new List<Category>();
    }
}

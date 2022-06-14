using System;
using System.Linq;
using UnityEditor.IMGUI.Controls;

namespace Ape.Scriptable
{
    internal class ScriptableDropdownItem : AdvancedDropdownItem
    {
        private ScriptableDatabase.Category _category;
        private ScriptableBase _scriptable;

        public ScriptableBase Scriptable => _scriptable;

        public ScriptableDropdownItem(ScriptableDatabase.Category category, Type type)
            : base(category.Name)
        {
            var scriptables = category.Scriptables.Where(
                scriptable => scriptable.GetType().IsSubclassOf(type)
            );
            foreach (var scriptable in scriptables)
            {
                var dropdownItem = new ScriptableDropdownItem(scriptable);
                AddChild(dropdownItem);
            }

            _category = category;
        }

        public ScriptableDropdownItem(ScriptableBase scriptable) : base(scriptable.name) =>
            _scriptable = scriptable;
    }
}

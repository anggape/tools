using System;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace Ape.Scriptable
{
    internal class ScriptableDropdown : AdvancedDropdown
    {
        private Type _type;
        private Action<ScriptableBase> _callback;

        public ScriptableDropdown(Type type, Action<ScriptableBase> callback)
            : base(new AdvancedDropdownState())
        {
            _type = type;
            _callback = callback;
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("Scriptables");
            var databaseGuids = AssetDatabase.FindAssets(
                $"t:{typeof(ScriptableDatabase).FullName}"
            );

            foreach (var databaseGuid in databaseGuids)
            {
                var databasePath = AssetDatabase.GUIDToAssetPath(databaseGuid);
                var database =
                    AssetDatabase.LoadMainAssetAtPath(databasePath) as ScriptableDatabase;
                foreach (var category in database.Categories)
                {
                    var hasTypes =
                        category.Scriptables
                            .Where(scriptable => scriptable.GetType().IsSubclassOf(_type))
                            .FirstOrDefault() != null;

                    if (category.Scriptables.Length > 0 && hasTypes)
                        root.AddChild(new ScriptableDropdownItem(category, _type));
                }
            }

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            var dropdownItem = item as ScriptableDropdownItem;
            if (dropdownItem.Scriptable != null)
                _callback?.Invoke(dropdownItem.Scriptable);
        }
    }
}

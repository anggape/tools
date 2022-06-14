using UnityEditor;
using UnityEngine;

namespace Ape.Scriptable
{
    [CustomPropertyDrawer(typeof(ScriptableBase), true)]
    public class ScriptableBaseDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rect = EditorGUI.PrefixLabel(position, label);
            var value = property.objectReferenceValue;
            var buttonTitle = value != null ? value.name : "[None]";

            if (EditorGUI.DropdownButton(rect, new GUIContent(buttonTitle), FocusType.Passive))
            {
                var dropdown = new ScriptableDropdown(
                    fieldInfo.FieldType,
                    scriptable =>
                    {
                        property.objectReferenceValue = scriptable;
                        property.serializedObject.ApplyModifiedProperties();
                    }
                );
                dropdown.Show(position);
            }
        }
    }
}

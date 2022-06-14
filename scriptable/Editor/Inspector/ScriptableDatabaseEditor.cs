using UnityEditor;

namespace Ape.Scriptable
{
    [CustomEditor(typeof(ScriptableDatabase))]
    public class ScriptableDatabaseEditor : Editor
    {
        public override void OnInspectorGUI() =>
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_categories"));
    }
}

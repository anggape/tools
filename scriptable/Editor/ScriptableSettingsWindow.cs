using Ape.Scriptable;
using UnityEditor;

public class ScriptableSettingsWindow : EditorWindow
{
    [MenuItem("Ape/Scriptable/Settings")]
    private static void Open() => GetWindow<ScriptableSettingsWindow>("Scriptable Settings");

    private void OnGUI()
    {
        // TODO: do not save on every update?
        EditorGUILayout.BeginVertical("box");
        ScriptableSettings.ScriptsOutput = EditorGUILayout.TextField(
            "Scripts Output",
            ScriptableSettings.ScriptsOutput
        );
        ScriptableSettings.AssetsOutput = EditorGUILayout.TextField(
            "Assets Output",
            ScriptableSettings.AssetsOutput
        );
        EditorGUILayout.EndVertical();
    }
}

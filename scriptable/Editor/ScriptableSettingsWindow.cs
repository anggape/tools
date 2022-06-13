using UnityEditor;
using UnityEngine;

namespace Ape.Scriptable
{
    public class ScriptableSettingsWindow : EditorWindow
    {
        private ScriptableGroup _assetGroupSetting;
        private string _scriptsOutputSetting;

        [MenuItem("Ape/Scriptable/Settings")]
        private static void Open() => GetWindow<ScriptableSettingsWindow>("Scriptable Settings");

        private void OnEnable()
        {
            _assetGroupSetting = ScriptableSettings.AssetGroup;
            _scriptsOutputSetting = ScriptableSettings.ScriptsOutput;
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical("box");
            _scriptsOutputSetting = EditorGUILayout.TextField(
                "Scripts Output",
                _scriptsOutputSetting
            );
            _assetGroupSetting =
                EditorGUILayout.ObjectField(
                    "Assets Output",
                    _assetGroupSetting,
                    typeof(ScriptableGroup),
                    false
                ) as ScriptableGroup;

            if (GUILayout.Button("Save"))
            {
                ScriptableSettings.ScriptsOutput = _scriptsOutputSetting;
                if (_assetGroupSetting != null)
                    ScriptableSettings.AssetGroup = _assetGroupSetting;
                ScriptableSettings.Save();
            }

            EditorGUILayout.EndVertical();
        }
    }
}

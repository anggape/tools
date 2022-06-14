using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Ape.Scriptable
{
    public class ScriptableSettingsWindow : EditorWindow
    {
        private ScriptableDatabase _database;
        private Editor _databaseEditor;
        private string _scriptsOutputSetting;

        [MenuItem("Ape/Scriptable/Settings")]
        private static void Open() => GetWindow<ScriptableSettingsWindow>("Scriptable Settings");

        private void OnEnable()
        {
            _scriptsOutputSetting = ScriptableSettings.ScriptsOutput;

            var databaseGuids = AssetDatabase.FindAssets(
                $"t:{typeof(ScriptableDatabase).FullName}"
            );

            if (databaseGuids.Length == 0)
            {
                _database = ScriptableObject.CreateInstance<ScriptableDatabase>();
                AssetDatabase.CreateAsset(
                    _database,
                    Path.Combine("Assets", $"{nameof(ScriptableDatabase)}.asset")
                );
                AssetDatabase.Refresh();
            }
            else
            {
                var path = AssetDatabase.GUIDToAssetPath(databaseGuids.First());
                _database = AssetDatabase.LoadMainAssetAtPath(path) as ScriptableDatabase;
            }

            if (databaseGuids.Length > 1)
                Debug.LogWarning("multiple scriptable database detected.");

            Debug.Log(_database.name);

            _databaseEditor = Editor.CreateEditor(_database);
        }

        private void OnGUI()
        {
            _scriptsOutputSetting = EditorGUILayout.TextField(
                "Scripts Output",
                _scriptsOutputSetting
            );

            // TODO: separate settings and categories
            _databaseEditor.OnInspectorGUI();

            if (GUILayout.Button("Save"))
            {
                ScriptableSettings.ScriptsOutput = _scriptsOutputSetting;
                ScriptableSettings.Save();
            }
        }
    }
}

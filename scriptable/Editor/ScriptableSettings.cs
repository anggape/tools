using UnityEditor;
using UnityEditor.SettingsManagement;

namespace Ape.Scriptable
{
    public static class ScriptableSettings
    {
        private static Settings s_settings;

        public static string ScriptsOutput
        {
            get => Get(nameof(ScriptsOutput), "Assets/Scripts");
            set => Set(nameof(ScriptsOutput), value);
        }

        public static ScriptableGroup AssetGroup
        {
            get => GetScriptableGroup();
            set => SetScriptableGroup(value);
        }

        static ScriptableSettings() => s_settings = new Settings("com.anggape.tools.scriptable");

        private static T Get<T>(string key, T defaultValue) =>
            s_settings.Get<T>(key, fallback: defaultValue);

        private static void Set<T>(string key, T value) => s_settings.Set<T>(key, value);

        private static ScriptableGroup GetScriptableGroup()
        {
            var groupGuid = s_settings.Get<string>(nameof(AssetGroup));
            var groupPath = AssetDatabase.GUIDToAssetPath(groupGuid);
            return AssetDatabase.LoadMainAssetAtPath(groupPath) as ScriptableGroup;
        }

        private static void SetScriptableGroup(ScriptableGroup value)
        {
            if (value == null)
                return;

            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(value, out string guid, out long _);
            s_settings.Set(nameof(AssetGroup), guid);
        }

        public static void Save() => s_settings.Save();
    }
}

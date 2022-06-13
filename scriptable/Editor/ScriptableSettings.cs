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

        public static string AssetGroup
        {
            get => Get(nameof(AssetGroup), "");
            set => Set(nameof(AssetGroup), value);
        }

        static ScriptableSettings() => s_settings = new Settings("com.anggape.tools.scriptable");

        private static T Get<T>(string key, T defaultValue) =>
            s_settings.Get<T>(key, fallback: defaultValue);

        private static void Set<T>(string key, T value) => s_settings.Set<T>(key, value);

        public static void Save() => s_settings.Save();
    }
}

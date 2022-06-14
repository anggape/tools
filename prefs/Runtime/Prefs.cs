using Newtonsoft.Json;
using UnityEngine;

namespace Ape
{
    public static class Prefs
    {
        public const string PrefsPrefix = "__ape_prefs:";

        public static void Set(string key, object value)
        {
            var json = JsonConvert.SerializeObject(
                value,
                Formatting.None,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            );

            PlayerPrefs.SetString($"{PrefsPrefix}{key}", json);
        }

        public static T Get<T>(string key)
        {
            var json = PlayerPrefs.GetString($"{PrefsPrefix}{key}", "{}");
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

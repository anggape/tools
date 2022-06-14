using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Ape.Scriptable
{
    [InitializeOnLoad]
    public static class ScriptableCodeGen
    {
        private static List<Type> _scriptableTypes = new List<Type>();

        static ScriptableCodeGen()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
                ProcessAssembly(assembly);
        }

        private static void ProcessAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();

            _scriptableTypes.AddRange(types.Where(x => x.IsScriptable()).Where(x => !x.IsAbstract));

            foreach (var type in types)
                ProcessType(type);
        }

        private static void ProcessType(Type type)
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            var fields = type.GetFields(bindingFlags)
                .Where(x => x.FieldType.IsScriptable())
                .Where(x => x.FieldType.IsAbstract);

            foreach (var field in fields)
                ProcessField(field);
        }

        private static void ProcessField(FieldInfo field)
        {
            if (field.FieldType.HasNonAbstract())
                return;

            var typeName = field.FieldType.Name;
            var typeFullName = field.FieldType.FullName;
            var genericTypes = field.FieldType.GenericTypeArguments;

            var className = string.Join(string.Empty, genericTypes.Select(x => x.Name));
            var scriptableType = typeName.Substring(0, typeName.IndexOf('`'));
            var scriptableBase = typeFullName.Substring(0, typeFullName.IndexOf('`'));
            var inherits = string.Join(", ", genericTypes.Select(x => x.FullName));

            var scriptDirectory = ScriptableSettings.ScriptsOutput;
            var scriptFile = Path.Combine(scriptDirectory, $"{className}{scriptableType}.cs");
            var script = string.Format(
                "public class {0}{1} : {2}<{3}> {{ }}",
                className,
                scriptableType,
                scriptableBase,
                inherits
            );

            if (!Directory.Exists(scriptDirectory))
                Directory.CreateDirectory(scriptDirectory);

            using (var writer = new StreamWriter(scriptFile))
                writer.WriteLine(script);
        }

        private static bool IsScriptable(this Type type) =>
            type.IsSubclassOf(typeof(ScriptableBase));

        private static bool HasNonAbstract(this Type type) =>
            _scriptableTypes.FirstOrDefault(x => x.IsSubclassOf(type)) != null;
    }
}

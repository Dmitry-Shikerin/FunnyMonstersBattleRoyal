using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.CodeGen
{
    public static class EnumGenerator
    {
        private static string s_path;
        private static string s_name;
        private static List<string> s_enumNames;

        public static void SetPath(string path)
        {
            s_path = path;
        }

        public static void SetName(string name)
        {
            s_name = name;
        }

        public static void SetEnumNames(List<string> names)
        {
            s_enumNames = new List<string>();
            s_enumNames.Add("Default");
            s_enumNames.AddRange(names);
        }

        public static void Generate()
        {
            int i = 0;
            string path = $"{Application.dataPath}/{s_path}/{s_name}.cs";
            string code = $@"namespace {s_path.Replace("/", ".")}
{{
   public enum {s_name} 
   {{
{
    String.Join("\n",
        s_enumNames
            .Where(name => name != "")
            .Select(name => $"      {name.Replace(" ", "")} = {i++},"))
}
   }}
}}";
            
            File.WriteAllText(path, code);
        }
        
        public static void GenerateLayers()
        {
            string path = $"{Application.dataPath}/Sources/Generated/Layers.cs";
            string code = $@"public static class Layers 
{{
{
    String.Join("\n",
        GetLayerNames()
            .Where(layerName => layerName != "")
            .Select(layerName => $"const string {layerName.Replace(" ", "_").ToUpper()} = \"{layerName}\";"))
}
}}";

            // Записываем код в файл
            System.IO.File.WriteAllText(path, code);
        }

        private static string[] GetLayerNames()
        {
            string[] layers = new string[32];

            for (int i = 0; i < 32; i++)
                layers[i] = LayerMask.LayerToName(i);

            return layers;
        }
    }
}
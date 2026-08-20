using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.MyUiFramework.Utils
{
    public static class MyAssetUtils
    {
        /// <summary>
        /// Returns a reference to a scriptable object of type T with the given fileName at the relative resourcesPath.
        /// <para/> If the asset is not found, one will get created automatically (in the Editor only) 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="resourcesPath"></param>
        /// <param name="saveAssetDatabase"></param>
        /// <param name="refreshAssetDatabase"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetScriptableObject<T>(
            string fileName, string resourcesPath, bool saveAssetDatabase, bool refreshAssetDatabase)
            where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(resourcesPath))
                return null;
            
            if (string.IsNullOrEmpty(fileName))
                return null;
            
            // ReSharper disable once SuspiciousTypeConversion.Global
            // if (resourcesPath[resourcesPath.Length - 1].Equals(@"\") == false)
            //     resourcesPath += @"\";
            
            resourcesPath = resourcesPath.Replace(@"\", "/");
            resourcesPath = CleanPath(resourcesPath);

            T obj = (T) Resources.Load(fileName, typeof(T));

            if (obj == null)
            {
                string simpleResourcesPath = resourcesPath.Replace(
                    resourcesPath.Substring(0, resourcesPath.LastIndexOf(
                        "Resources", StringComparison.Ordinal)), "");
                simpleResourcesPath = simpleResourcesPath.Replace(
                    "Resources", "").Remove(0, 1);
                obj = (T) Resources.Load(Path.Combine(simpleResourcesPath, fileName), typeof(T));
            }

#if UNITY_EDITOR
            if (obj != null)
                return obj;
            
            obj = CreateAsset<T>(resourcesPath, fileName, saveAssetDatabase, refreshAssetDatabase);
#endif
            return obj;
        }

        public static T GetScriptableObject<T>(string relativePath, string fileName)
            where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(relativePath))
                throw new ArgumentNullException(nameof(relativePath));

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException(nameof(fileName));
            
            string path = CleanPath(relativePath);
#if UNITY_EDITOR
            // В Editor загружаем через AssetDatabase (можно создавать, если нет)
            if (Application.isPlaying == false)
                return CreateAsset<T>(path, fileName);
#endif

            // В Runtime или Play Mode загружаем из Resources (только чтение)
            T runtimeAsset = Resources.Load<T>($"{path}{fileName}");
    
            if (runtimeAsset == null)
                throw new ArgumentNullException($"Asset {fileName} not found in Resources at path {path}");
    
            return runtimeAsset;
        }

        public static T GetResource<T>(string resourcesPath, string fileName)
            where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(resourcesPath))
                return null;
            
            if (string.IsNullOrEmpty(fileName)) 
                return null;
            
            resourcesPath = CleanPath(resourcesPath);
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (!resourcesPath[resourcesPath.Length - 1].Equals(@"\")) resourcesPath += @"\";
            resourcesPath = resourcesPath.Replace(@"\", "/");

            return (T) Resources.Load(resourcesPath + fileName, typeof(T));
        }

        public static string CleanPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            // 1. Заменяем все обратные слеши на прямые
            path = path.Replace('\\', '/');
    
            // 2. Убираем дублирующиеся слеши (например, "//" -> "/")
            while (path.Contains("//"))
                path = path.Replace("//", "/");
    
            // 3. Убеждаемся, что путь заканчивается на '/'
            if (!path.EndsWith("/"))
                path += "/";
    
            return path;
        }

#if UNITY_EDITOR
        public static T CreateAsset<T>(
            string relativePath, 
            string fileName, 
            bool saveAssetDatabase = true, 
            bool refreshAssetDatabase = true) 
            where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(relativePath) || string.IsNullOrEmpty(fileName))
            {
                Debug.LogError("Path or file name is empty");
                return null;
            }

            // 1. Приводим путь к единому формату через CleanPath
            string cleanPath = CleanPath(relativePath);
    
            // 2. Полный путь с расширением .asset
            string fullPath = cleanPath + fileName + ".asset";

            // 3. Проверяем, существует ли уже такой файл
            T existingAsset = AssetDatabase.LoadAssetAtPath<T>(fullPath);
            if (existingAsset != null)
            {
                Debug.LogWarning($"Asset already exists at path: {fullPath}");
                return existingAsset;
            }

            // 4. Проверяем, существует ли папка. Если нет — создаём.
            string directoryPath = cleanPath.TrimEnd('/');
            if (!AssetDatabase.IsValidFolder(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                AssetDatabase.Refresh(); // Важно обновить, чтобы Unity увидел новую папку
            }

            // 5. Создаём ассет
            T asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, fullPath);
            EditorUtility.SetDirty(asset);

            if (saveAssetDatabase)
                AssetDatabase.SaveAssets();

            if (refreshAssetDatabase)
                AssetDatabase.Refresh();

            Debug.Log($"Asset created successfully: {fullPath}");
            return asset;
        }

        public static List<T> GetAssets<T>() where T : ScriptableObject
        {
            List<T> list = new List<T>();
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            
            foreach (string guid in guids)
            {
                T asset = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
                if (asset == null) continue;
                list.Add(asset);
            }

            return list;
        }
        
        public static void MoveAssetToTrash(
            string relativePath, 
            string fileName, 
            bool saveAssetDatabase = true,
            bool refreshAssetDatabase = true, 
            bool printDebugMessage = true)
        {
            if (string.IsNullOrEmpty(relativePath))
                return;
            
            if (string.IsNullOrEmpty(fileName))
                return;
            
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (!relativePath[relativePath.Length - 1].Equals(@"\"))
                relativePath += @"\";
            
            relativePath = CleanPath(relativePath);
            
            if (AssetDatabase.MoveAssetToTrash(relativePath + fileName + ".asset") == false)
                return;
            
            if (saveAssetDatabase)
                AssetDatabase.SaveAssets();
            
            if (refreshAssetDatabase)
                AssetDatabase.Refresh();
        }
#endif
    }
}
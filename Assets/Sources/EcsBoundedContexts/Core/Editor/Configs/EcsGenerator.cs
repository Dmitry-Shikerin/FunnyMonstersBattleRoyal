using System;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEditor;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Core.Editor.Configs
{
    [CreateAssetMenu(fileName = nameof(EcsGenerator), menuName = "Configs/" + nameof(EcsGenerator), order = 51)]
    public class EcsGenerator : ScriptableObject
    {
        [field: SerializeField] public AspectName DefaultAspectName { get; set; }
        [field: SerializeField] public string AspectPath { get; set; }
        
        private static EcsGenerator _instance;

        public static EcsGenerator Instance
        {
            get
            {
                if (_instance == null)
                {
                    string[] guids = AssetDatabase.FindAssets("t:EcsGenerator");

                    if (guids.Length > 1)
                        throw new InvalidOperationException("Multiple EcsGenerator assets found");

                    if (guids.Length > 0)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                        _instance = AssetDatabase.LoadAssetAtPath<EcsGenerator>(path);
                        return _instance;
                    }
                    
                    _instance = CreateInstance<EcsGenerator>();
                    AssetDatabase.CreateAsset(_instance, "Assets/EcsGenerator.asset");
                    AssetDatabase.SaveAssets();
                    return _instance;
                }

                return _instance;
            }
        }
    }
}
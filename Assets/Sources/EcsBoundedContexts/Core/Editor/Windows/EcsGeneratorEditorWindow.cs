using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Editor.Configs;
using Sources.EcsBoundedContexts.Core.Editor.Configs.Generate;
using UnityEditor;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Core.Editor.Windows
{
    public class EcsGeneratorEditorWindow : OdinEditorWindow
    {
        [OnValueChanged(nameof(ChangeDefaultAspectName))] 
        [SerializeField] private AspectName _defaultAspectName;

        [FolderPath(ParentFolder = "Assets")] 
        [OnValueChanged(nameof(ChangeAspectPath))] 
        [SerializeField] private string _aspectPath;

        [MenuItem("Tools/Ecs Generator")]
        private static void OpenWindow()
        {
            GetWindow<EcsGeneratorEditorWindow>().Show();
        }

        protected override void Initialize()
        {
            base.Initialize();
            EcsGenerator generator = EcsGenerator.Instance;

            _defaultAspectName = generator.DefaultAspectName;
            _aspectPath = generator.AspectPath;
        }

        [Button]
        public static void GenerateComponents()
        {
            AspectGenerator.GenerateAspect(AspectName.Game);
            AspectGenerator.GenerateAspectExt(AspectName.Game);
        }

        [Button]
        public static void GenerateSystems()
        {
            IEnumerable<AspectName> names = Enum.GetValues(typeof(AspectName)).Cast<AspectName>();

            foreach (AspectName name in names)
            {
                if (name == AspectName.Default)
                    continue;

                SystemsGenerator.GenerateSystemsCollector(name);
                SystemsGenerator.GenerateSystemsInstaller(name);
            }
            
        }

        private void ChangeDefaultAspectName(AspectName aspectName)
        {
            EcsGenerator.Instance.DefaultAspectName = aspectName;
            EditorUtility.SetDirty(EcsGenerator.Instance);
            AssetDatabase.SaveAssetIfDirty(EcsGenerator.Instance);
        }

        private void ChangeAspectPath(string path)
        {
            EcsGenerator.Instance.AspectPath = path;
            EditorUtility.SetDirty(EcsGenerator.Instance);
            AssetDatabase.SaveAssetIfDirty(EcsGenerator.Instance);
        }
    }
}
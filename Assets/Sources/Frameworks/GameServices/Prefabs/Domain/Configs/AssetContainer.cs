using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.Frameworks.GameServices.Prefabs.Domain.Configs
{
    [Serializable]
    public struct AssetContainer<T> 
        where T : MonoBehaviour
    {
#if UNITY_EDITOR
        [HorizontalGroup("Group", Width = 70)]
        [HideLabel]
        [OnValueChanged(nameof(OnAssetChanged))]
        [PreviewField(70, ObjectFieldAlignment.Left)]
        public T Asset;
#endif
        [VerticalGroup("Group/Path")]
        [HideLabel]
        [Sirenix.OdinInspector.FilePath(ParentFolder = "Assets/Resources")]
        public string FolderPath;

#if UNITY_EDITOR
        //[OnInspectorInit]
        private void Validate()
        {
            if (Asset != null && string.IsNullOrEmpty(FolderPath))
                OnAssetChanged(Asset);
        }

        [VerticalGroup("Group/Path")]
        [PropertySpace(12)]
        [Button("Refresh", ButtonSizes.Large)]
        private void Refresh() =>
            OnAssetChanged(Asset);
        
        private void OnAssetChanged(Object asset)
        {
            if (asset == null)
            {
                FolderPath = string.Empty;
                return;
            }

            string path = AssetDatabase.GetAssetPath(asset);
            string cleanPath = path.Replace(".prefab", "");
            cleanPath = cleanPath.Replace("Assets/Resources/", "");
            FolderPath = cleanPath.Replace(".asset", "");
        }
#endif
    }
}
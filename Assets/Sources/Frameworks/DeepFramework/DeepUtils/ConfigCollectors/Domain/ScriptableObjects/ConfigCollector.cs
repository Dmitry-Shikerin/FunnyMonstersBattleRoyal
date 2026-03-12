using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.MyUiFramework.Utils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects
{
    public class ConfigCollector<T> : ScriptableObject
        where T : Config
    {
        [FormerlySerializedAs("_enable")]
        [EnumToggleButtons] [HideLabel] [UsedImplicitly]
        [SerializeField] private EnableState enableState = EnableState.Off;
        [EnableIf(nameof(enableState), EnableState.On)]
        [SerializeField] private string _id;
        [EnableIf(nameof(enableState), EnableState.On)]
        [SerializeField] private List<T> _configs = new List<T>();
        [TabGroup("Create")]
        [ValidateInput("ValidateId")]
        [SerializeField] private string _addedConfigId;
        [TabGroup("Remove")]
        [ValueDropdown(nameof(GetRemovedId))]
        [SerializeField] private string _removedConfigId;

        public List<T> Configs => _configs;
        public string Id => _id;

        private bool ValidateId() =>
            _configs.Any(collector => collector.Id == _addedConfigId) == false;

        public void SetId(string id) =>
            _id = id;
        
        public T GetById(string id) =>
            _configs.First(config => config.Id == id);

#if UNITY_EDITOR
        [TabGroup("Remove")]
        [PropertySpace(10)] 
        [Button]
        public void RemoveConfig()
        {
            T config = Configs.FirstOrDefault(config => config.Id == _removedConfigId);
            AssetDatabase.RemoveObjectFromAsset(config);
            Configs.Remove(config);
            AssetDatabase.SaveAssets();
        }

        [TabGroup("Create")]
        [PropertySpace(10)] 
        [Button]
        private void CreateConfig()
        {
            if (string.IsNullOrWhiteSpace(_addedConfigId))
                return;

            if (string.IsNullOrEmpty(_addedConfigId))
                return;

            if (Configs.Any(config => config.Id == _addedConfigId))
            {
                EditorDialogUtils.ShowErrorDialog(
                    $"{typeof(T).Name} ConfigCollector",
                    $"This id not unique: {_addedConfigId}");

                return;
            }

            T config = CreateInstance<T>();
            config.SetParent(this);
            AssetDatabase.AddObjectToAsset(config, this);
            config.SetId(_addedConfigId);
            config.name = $"{_addedConfigId}_{typeof(T).Name}";
            Configs.Add(config);
            AssetDatabase.SaveAssets();
        }
#endif
        private List<string> GetRemovedId()
        {
#if UNITY_EDITOR
            return _configs.Select(config => config.Id).ToList();
#else
            return new List<string>();
#endif
        }
    }
}
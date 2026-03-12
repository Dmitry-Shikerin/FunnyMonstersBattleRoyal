using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Achievements.Domain.Data;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepLocalization.Domain.Data;
using Sources.Frameworks.DeepFramework.DeepLocalization.Runtime.Domain.Data;
using UnityEditor;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Achievements.Domain.Configs
{
    [CreateAssetMenu(
        fileName = nameof(AchievementConfigCollector), 
        menuName = "Configs/Achievements/" + nameof(AchievementConfigCollector), 
        order = 51)]
    public class AchievementConfigCollector : ScriptableObject
    {
        private const string ResponsiveButtonsGroup = "Buttons";
        
        [field: SerializeField] public List<AchievementConfig> Configs { get; private set; }
        [PropertySpace(10)]
        [ValueDropdown("GetId")]
        [SerializeField] private string _configId;
        [ValueDropdown("GetLocalisationId")]
        [SerializeField] private string _titleId;
        [ValueDropdown("GetLocalisationId")]
        [SerializeField] private string _descriptionId;
        [SerializeField] private Sprite _sprite;

        public AchievementConfig GetById(string id)
        {
            foreach (AchievementConfig config in Configs)
            {
                if (config.Id != id)
                    continue;
                
                return config;
            }
            
            throw new KeyNotFoundException($"AchievementConfig with id {id} not found");
        }
        
#if UNITY_EDITOR
        public void RemoveConfig(AchievementConfig wave)
        {
            AssetDatabase.RemoveObjectFromAsset(wave);
            Configs.Remove(wave);
            AssetDatabase.SaveAssets();
        }

        private IReadOnlyList<string> GetId() =>
            IdsConst.GetIds<AchievementSaveData>();

        [UsedImplicitly]
        [ResponsiveButtonGroup(ResponsiveButtonsGroup)]
        private void CreateConfig()
        {
            if (string.IsNullOrWhiteSpace(_configId))
                return;

            if (string.IsNullOrEmpty(_configId))
                return;

            if (Configs.Any(config => config.Id == _configId))
            {
                Debug.Log($"this id not unique: {_configId}");
                return;
            }
            
            AchievementConfig achievementConfig = CreateInstance<AchievementConfig>();
            achievementConfig.Parent = this;
            AssetDatabase.AddObjectToAsset(achievementConfig, this);
            achievementConfig.Id = _configId;
            achievementConfig.name = $"{_configId}_AchievementConfig";
            achievementConfig.TitleId = _titleId;
            achievementConfig.DescriptionId = _descriptionId;
            achievementConfig.Sprite = _sprite;
            Configs.Add(achievementConfig);
            AssetDatabase.SaveAssets();
        }

        public void CreateConfig(string id, string titleId, string descriptionId, Sprite sprite)
        {
            _configId = id;
            _titleId = titleId;
            _descriptionId = descriptionId;
            _sprite = sprite;
            CreateConfig();
        }
        
        private List<string> GetLocalisationId() =>
            LocalizationDataBase.Instance.Phrases
                .Select(phrase => phrase.LocalizationId)
                .ToList();
#endif
    }
}
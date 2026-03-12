using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepCores.Domain;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.EcsBoundedContexts.Upgrades.Domain.Configs
{
    public class UpgradeConfig : ScriptableObject
    {
        [FormerlySerializedAs("_enable")]
        [EnumToggleButtons] [HideLabel] [UsedImplicitly]
        [SerializeField] private EnableState enableState = EnableState.Off;
        [EnableIf("enableState", EnableState.On)]
        [SerializeField] private UpgradeConfigContainer _parent;
        [EnableIf("enableState", EnableState.On)]
        [SerializeField] private string _upgradeId;
        [EnableIf("enableState", EnableState.On)]
        [SerializeField] private List<UpgradeLevel> _upgradeLevels;

        public string Id => _upgradeId;
        public List<UpgradeLevel> Levels => _upgradeLevels;
        
#if UNITY_EDITOR
        public void RemoveLevel(UpgradeLevel wave)
        {
            AssetDatabase.RemoveObjectFromAsset(wave);
            _upgradeLevels.Remove(wave);
            RenameWaves();
            AssetDatabase.SaveAssets();
        }

        public void SetParent(UpgradeConfigContainer upgradeConfigContainer) =>
            _parent = upgradeConfigContainer ?? throw new NullReferenceException("Parent is null");

        private void RenameWaves()
        {
            for (int i = 0; i < _upgradeLevels.Count; i++)
            {
                _upgradeLevels[i].name = $"Level_{i + 1}";
                _upgradeLevels[i].SetLevelId(i + 1);
            }
            
            AssetDatabase.SaveAssets();
        }

        [UsedImplicitly]
        [ResponsiveButtonGroup("Buttons")]
        private void CreateLevel()
        {
            UpgradeLevel level = CreateInstance<UpgradeLevel>();
            int levelId = _upgradeLevels.Count + 1;
            level.SetParent(this);
            AssetDatabase.AddObjectToAsset(level, this);
            level.SetLevelId(levelId);
            level.name = $"Level_{levelId}";
            _upgradeLevels.Add(level);
            AssetDatabase.SaveAssets();

        }

        [PropertySpace(20)]
        [Button(ButtonSizes.Medium)]
        private void RemoveConfig() =>
            _parent.RemoveUpgradeConfig(this);

        public void SetUpgradeId(string upgradeId) =>
            _upgradeId = upgradeId;
#endif
    }
}
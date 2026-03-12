using System.Collections.Generic;
using System.Linq;
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
    [CreateAssetMenu(fileName = nameof(UpgradeConfigContainer), menuName = "Configs/" + nameof(UpgradeConfigContainer), order = 51)]
    public class UpgradeConfigContainer : ScriptableObject
    {
        [FormerlySerializedAs("_enable")]
        [Header("Data")]
        [EnumToggleButtons] [HideLabel] [UsedImplicitly]
        [SerializeField] private EnableState enableState = EnableState.Off;
        [EnableIf("enableState", EnableState.On)]
        [SerializeField] private List<UpgradeConfig> _upgradeConfigs;
        [Header("CreateConfig")] [ValidateInput("ValidateId")]
        [SerializeField] private string _upgradeId;

        public IReadOnlyList<UpgradeConfig> UpgradeConfigs => _upgradeConfigs;

#if UNITY_EDITOR
        public void RemoveUpgradeConfig(UpgradeConfig upgradeConfig)
        {
            string path = AssetDatabase.GetAssetPath(upgradeConfig);
            _upgradeConfigs.Remove(upgradeConfig);
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.SaveAssets();
        }
        
        [UsedImplicitly]
        [ResponsiveButtonGroup("Buttons")]
        private void CreateUpgradeConfig()
        {
            UpgradeConfig upgradeConfig = CreateInstance<UpgradeConfig>();
            AssetDatabase.CreateAsset(upgradeConfig, "Assets/Resources/Configs/Upgrades/Configs/UpgradeConfig.asset");
            string path = AssetDatabase.GetAssetPath(upgradeConfig);
            AssetDatabase.RenameAsset(path, $"{_upgradeId}_UpgradeConfig");
            upgradeConfig.SetParent(this);
            upgradeConfig.SetUpgradeId(_upgradeId);
            _upgradeConfigs.Add(upgradeConfig);
            AssetDatabase.SaveAssets();
        }

        private bool ValidateId() =>
            _upgradeConfigs.Any(upgradeConfig => upgradeConfig.Id == _upgradeId) == false;
#endif
    }
}
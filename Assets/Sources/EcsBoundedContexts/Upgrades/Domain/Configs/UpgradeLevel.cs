using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepCores.Domain;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils;
using Sources.Frameworks.DeepFramework.DeepUtils.Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.EcsBoundedContexts.Upgrades.Domain.Configs
{
    public class UpgradeLevel : ScriptableObject
    {
        [FormerlySerializedAs("_enable")]
        [EnumToggleButtons] [HideLabel] [UsedImplicitly]
        [SerializeField] private EnableState enableState = EnableState.Off;
        [EnableIf("enableState", EnableState.On)]
        [SerializeField] private int _id;
        [EnableIf("enableState", EnableState.On)]
        [SerializeField] private UpgradeConfig _parent;
        [Space(10)]
        [SerializeField] private int _moneyPerUpgrade;
        [SerializeField] private float _currentAmount;

        public int MoneyPerUpgrade => _moneyPerUpgrade;
        public float CurrentAmount => _currentAmount;
        public int Id => _id;

        public void SetLevelId(int id) =>
            _id = id;
        
        public void SetParent(UpgradeConfig parent) =>
            _parent = parent;

#if UNITY_EDITOR
        [PropertySpace(7)]
        [Button(ButtonSizes.Medium)]
        private void Remove() =>
            _parent.RemoveLevel(this);
#endif
    }
}
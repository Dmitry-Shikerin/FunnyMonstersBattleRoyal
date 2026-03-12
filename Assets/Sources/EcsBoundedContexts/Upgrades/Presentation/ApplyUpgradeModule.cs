using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Upgrades.Presentation
{
    public class ApplyUpgradeModule : EntityModule
    {
        [field: Required] [field: SerializeField] public Button Button { get; private set; }
        [field: Required] [field: SerializeField] public TMP_Text NextUpgradePriceText { get; private set; }
        [field: Required] [field: SerializeField] public Image SkullImage { get; private set; }

        private void OnEnable()
        {
            if (IsInitialized == false)
                return;
            
            Button.onClick.AddListener(ApplyUpgrade);
        }

        protected override void OnAfterDisable()
        {
            Button.onClick.RemoveListener(ApplyUpgrade);
        }

        private void ApplyUpgrade()
        {
            if (Entity.HasApplyUpgradeEvent())
                return;
            
            Entity.AddApplyUpgradeEvent();
        }
    }
}
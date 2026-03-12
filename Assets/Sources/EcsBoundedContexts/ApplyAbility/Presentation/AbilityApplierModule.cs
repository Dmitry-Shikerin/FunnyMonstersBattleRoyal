using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.ApplyAbility.Presentation
{
    public class AbilityApplierModule : EntityModule
    {
        [field: Required] [field: SerializeField] public Button Button { get; private set; }
        [field: Required] [field: SerializeField] public Image Image { get; private set; }

        private void OnEnable()
        {
            Button.onClick.AddListener(ApplyAbility);
        }

        protected override void OnAfterDisable()
        {
            Button.onClick.RemoveListener(ApplyAbility);
        }

        private void ApplyAbility()
        {
            if (Entity.HasChangeForDurationTime())
                return;
            
            Entity.AddApplyAbilityEvent();
        }
    }
}
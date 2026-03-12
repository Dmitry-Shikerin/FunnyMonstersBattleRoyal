using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.ApplyAbility.Domain;
using Sources.EcsBoundedContexts.ApplyAbility.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Upgrades.Domain.Components;
using Sources.Frameworks.DeepFramework.DeepTwens.Eases;
using UnityEngine;

namespace Sources.EcsBoundedContexts.ApplyAbility.Controllers
{
    [EcsSystem(5)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class AbilityApplierSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                AbilityTag,
                ApplyAbilityEvent>());
        [DI] private readonly ProtoIt _timerIt = new(
            It.Inc<
                AbilityTag,
                ChangeForDurationTimeComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                float duration = GetDuration(entity);
                entity.AddChangeForDurationTime(duration, 0, 1, 0, 0);
                entity.GetAbilityApplierModule().Value.Button.interactable = false;
                
                if (entity.HasFirstUsedCompleted())
                    continue;
                
                entity.AddFirstUsedCompleted();
            }

            foreach (ProtoEntity entity in _timerIt)
            {
                ref ChangeForDurationTimeComponent changeForDurationTimeComponent =
                    ref entity.GetChangeForDurationTime();
                AbilityApplierModule module = entity.GetAbilityApplierModule().Value;

                int animationTimeLength = 1;
                float duration = changeForDurationTimeComponent.Duration;
                changeForDurationTimeComponent.AnimationTime += (Time.deltaTime / duration);

                float delta = EaseManager.Evaluate(Ease.Linear, changeForDurationTimeComponent.AnimationTime);
                float from = changeForDurationTimeComponent.FromValue;
                float to = changeForDurationTimeComponent.TargetValue;
                changeForDurationTimeComponent.Value = Mathf.Lerp(from, to, delta);
                module.Image.fillAmount = changeForDurationTimeComponent.Value;

                if (changeForDurationTimeComponent.AnimationTime < animationTimeLength)
                    continue;

                entity.DelChangeForDurationTime();
                module.Button.interactable = true;
            }
        }

        private float GetDuration(ProtoEntity entity)
        {
            if (entity.HasUpgradeLink())
            {
                UpgradeConfigComponent configComponent = entity.GetUpgradeLink().Value.GetUpgradeConfig();
                return configComponent.Value.Levels[configComponent.Index].CurrentAmount;
            }

            return entity.GetAbilityCooldownDuration().Value;
        }
    }
}
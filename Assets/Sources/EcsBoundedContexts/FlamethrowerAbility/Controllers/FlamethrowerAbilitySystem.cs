using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.ApplyAbility.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.FlamethrowerAbility.Domain;
using Sources.EcsBoundedContexts.Movements.Move.Components;
using Sources.Frameworks.DeepFramework.DeepSound.Runtime.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using UnityEngine;

namespace Sources.EcsBoundedContexts.FlamethrowerAbility.Controllers
{
    [EcsSystem(66)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class FlamethrowerAbilitySystem : IProtoRunSystem
    {
        private readonly ISoundService _soundService;

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                FlamethrowerAbilityTag,
                ApplyAbilityEvent>());
        [DI] private readonly ProtoIt _flamethrowerCompletePathIt = new(
            It.Inc<
                FlamethrowerTag,
                CompleteMoveAlongPathEvent>());

        public FlamethrowerAbilitySystem(ISoundService soundService)
        {
            _soundService = soundService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ProtoEntity flamethrower = entity.GetFlamethrowerLink().Value;
                Vector3 targetPoint = flamethrower.GetPointPath().Points[1];
                Vector3 startPoint = flamethrower.GetPointPath().Points[0];
                flamethrower.GetTransform().Value.position = startPoint;
                flamethrower.AddTargetPoint(targetPoint);
                flamethrower.ReplaceTargetPointIndex(0);
                flamethrower.GetFlameParticle().Value.Play();
                _soundService.Play(SoundDatabaseName.Sounds, SoundName.Flamethrower);
            }

            foreach (ProtoEntity entity in _flamethrowerCompletePathIt)
            {
                entity.GetFlameParticle().Value.Stop();
                _soundService.Stop(SoundName.Flamethrower);
            }
        }
    }
}
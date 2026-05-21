using DG.Tweening;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Components;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(55)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class GroundedSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IAssetCollector _collector;
        private CharacterConfig _config;

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                CharacterTag>());

        public GroundedSystem(IAssetCollector collector)
        {
            _collector = collector;
        }

        public void Init(IProtoSystems systems)
        {
            _config = _collector.Get<CharacterConfig>();
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                Transform groundCheck = entity.GetCharacterModule().Value.GroundCheck;
                bool isGrounded = Physics.CheckSphere(groundCheck.position, _config.GroundRadius, _config.GroundMask);

                if (isGrounded && entity.HasGrounded() == false)
                    entity.AddGrounded();
                else if (isGrounded == false && entity.HasGrounded()) 
                    entity.DelGrounded();
                // // "Прилипание" к земле: если на земле и скорость падения вниз, сбрасываем её
                // if (isGrounded && velocity.y < 0)
                // {
                //     velocity.y = -2f; // Небольшое прижатие, чтобы не отрывался от склонов
                // }
            }
        }
    }
}
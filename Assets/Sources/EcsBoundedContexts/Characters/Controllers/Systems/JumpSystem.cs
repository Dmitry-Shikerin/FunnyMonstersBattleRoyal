using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.DeepFramework.DeepTwens.Eases;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(55)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class JumpSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                CharacterTag,
                JumpingComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ref JumpingComponent jumping = ref entity.GetJumping();
                CharacterConfig config = entity.GetCharacterConfig().Value;
                Transform transform = entity.GetTransform().Value;
                float currentHeight = transform.position.y;
                float targetHeight = jumping.StartPos.y + config.JumpHeight;

                jumping.JumpTimer -= Time.deltaTime;
                transform.position += new Vector3(0, 5f * Time.deltaTime, 0);

                // Завершение прыжка
                if (currentHeight < targetHeight)
                    continue;

                entity.DelJumping();
            }
        }
    }
}
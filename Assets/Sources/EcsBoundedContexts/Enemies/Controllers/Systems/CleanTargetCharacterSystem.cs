using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Enemies.Domain.Components;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Systems
{
    [EcsSystem(61)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class CleanTargetCharacterSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                TargetCharacterComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ProtoEntity targetCharacter = entity.GetTargetCharacter().Value;
                
                if (targetCharacter.HasHealth() == false)
                {
                    entity.DelTargetCharacter();
                    continue;
                }
                
                int health = entity.GetTargetCharacter().Value.GetHealth().Value;

                if (health > 0)
                    continue;

                entity.DelTargetCharacter();
            }
        }
    }
}
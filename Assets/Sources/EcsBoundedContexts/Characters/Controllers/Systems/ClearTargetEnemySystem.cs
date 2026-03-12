using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(52)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class ClearTargetEnemySystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                CharacterTag,
                TargetEnemyComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ProtoEntity targetEnemy = entity.GetTargetEnemy().Value;
                
                if (targetEnemy.HasHealth() == false)
                {
                    entity.DelTargetEnemy();
                    continue;
                }
                
                int health = targetEnemy.GetHealth().Value;
                
                if (health > 0)
                    continue;
                
                entity.DelTargetEnemy();
            }
        }
    }
}
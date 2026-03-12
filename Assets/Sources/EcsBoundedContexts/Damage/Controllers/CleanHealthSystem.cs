using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Damage.Domain;

namespace Sources.EcsBoundedContexts.Damage.Controllers
{
    [EcsSystem(60)]
    [ComponentGroup(ComponentGroup.Player)]
    [Aspect(AspectName.Game)]
    public class CleanHealthSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new (
            It.Inc<
                HealthComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                int health = entity.GetHealth().Value;

                if (health > 0)
                    continue;
                
                entity.DelHealth();
            }
        }
    }
}
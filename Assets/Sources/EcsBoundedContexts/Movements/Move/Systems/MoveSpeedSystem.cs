using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Movements.Move.Components;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Move.Systems
{
    [EcsSystem(72)]
    [ComponentGroup(ComponentGroup.Player)]
    [Aspect(AspectName.Game)]
    public class MoveSpeedSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoItExc _it = new (
            It.Inc<
            MoveSpeedComponent,
            TargetSpeedComponent,
            ChangeSpeedDeltaComponent>(), 
            It.Exc<
            NavMeshComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ref MoveSpeedComponent moveSpeedComponent = ref entity.GetMoveSpeed();
                float targetSpeed = entity.GetTargetSpeed().Value;
                float speedDelta = entity.GetChangeSpeedDelta().Value * Time.deltaTime;
                
                moveSpeedComponent.Value = Mathf.MoveTowards(moveSpeedComponent.Value, targetSpeed, speedDelta);

                if (Mathf.Approximately(moveSpeedComponent.Value, targetSpeed) == false)
                    continue;
                
                entity.DelTargetSpeed();
            }
        }
    }
}
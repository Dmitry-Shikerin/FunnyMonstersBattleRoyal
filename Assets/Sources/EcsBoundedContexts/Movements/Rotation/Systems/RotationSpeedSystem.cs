using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Movements.Rotation.Components;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Rotation.Systems
{
    [EcsSystem(73)]
    [ComponentGroup(ComponentGroup.Player)]
    [Aspect(AspectName.Game)]
    public class RotationSpeedSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new (
            It.Inc<
                RotationSpeedComponent,
                TargetRotationSpeedComponent,
                ChangeRotationSpeedDeltaComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ref RotationSpeedComponent rotationSpeed = ref entity.GetRotationSpeed();
                float targetSpeed = entity.GetTargetRotationSpeed().Value;
                float speedDelta = entity.GetChangeRotationSpeedDelta().Value * Time.deltaTime;
                
                rotationSpeed.Value = Mathf.MoveTowards(rotationSpeed.Value, targetSpeed, speedDelta);

                if (Mathf.Approximately(rotationSpeed.Value, targetSpeed) == false)
                    continue;
                
                entity.DelTargetRotationSpeed();
            }
        }
    }
}
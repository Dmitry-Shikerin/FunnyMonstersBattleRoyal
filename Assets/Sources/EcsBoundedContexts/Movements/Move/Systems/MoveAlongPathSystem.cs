using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Movements.Move.Components;
using Sources.EcsBoundedContexts.Movements.TargetPoint.Components;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Move.Systems
{
    [EcsSystem(58)]
    [ComponentGroup(ComponentGroup.Light)]
    [Aspect(AspectName.Game)]
    public class MoveAlongPathSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                TransformComponent,
                PointPathComponent,
                TargetPointComponent,
                MoveSpeedComponent,
                TargetPointIndexComponent>());

        [DI] private readonly GameAspect _aspect;

        public void Run()
        {
            foreach (ProtoEntity entity in _protoIt)
            {
                ref TargetPointComponent targetPointComponent = ref entity.GetTargetPoint();
                ref TargetPointIndexComponent targetPointIndexComponent = ref entity.GetTargetPointIndex();

                Transform transform = entity.GetTransform().Value;
                Vector3[] points = entity.GetPointPath().Points;
                Vector3 targetPoint = targetPointComponent.Value;
                float moveSpeed = entity.GetMoveSpeed().Value;

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPoint,
                    moveSpeed * Time.deltaTime);

                Vector3 targetDirection = targetPoint - transform.position;
                float angle = Vector3.SignedAngle(Vector3.forward, targetDirection, Vector3.up);
                Quaternion targetRotation = Quaternion.Euler(0, angle, 0);

                if (entity.HasRotationSpeed())
                {
                    float rotationSpeed = entity.GetRotationSpeed().Value;
                    transform.rotation = Quaternion.RotateTowards(
                        transform.rotation,
                        targetRotation,
                        rotationSpeed * Time.deltaTime);
                }
                
                if (transform.position == points[^1])
                {
                    entity.DelTargetPoint();
                    entity.AddCompleteMoveAlongPathEvent();

                    continue;
                }

                if (transform.position != targetPoint)
                    continue;

                if (targetPointIndexComponent.Value == points.Length - 1)
                    continue;

                entity.AddCompleteMoveAlongPathPointEvent(targetPointIndexComponent.Value);
                targetPointIndexComponent.Value++;
                targetPointComponent.Value = points[targetPointIndexComponent.Value];
            }
        }
    }
}
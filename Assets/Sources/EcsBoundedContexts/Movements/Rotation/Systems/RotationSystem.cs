using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Movements.Rotation.Components;
using Sources.Frameworks.DeepFramework.DeepUtils.Constants;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.Rotation.Systems
{
    [EcsSystem(74)]
    [ComponentGroup(ComponentGroup.Player)]
    [Aspect(AspectName.Game)]
    public class RotationSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                TransformComponent,
                TargetRotationComponent,
                RotationSpeedComponent>());

        [DI] private readonly ProtoIt _itNext = new(
            It.Inc<
                TransformComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                Transform transform = entity.GetTransform().Value;
                Quaternion targetRotation = entity.GetTargetRotation().Value;
                float rotationSpeed = entity.GetRotationSpeed().Value * Time.deltaTime;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);

                if (Quaternion.Angle(transform.rotation, targetRotation) > MathConst.Epsilon)
                    continue;

                entity.DelTargetRotation();
            }
        }
    }
}
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(55)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class RotationToEnemySystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                CharacterTag,
                TargetEnemyComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                Vector3 enemyPosition = entity.GetTargetEnemy().Value.GetTransform().Value.position;
                Vector3 characterPosition = entity.GetTransform().Value.position;
                Quaternion characterRotation = entity.GetTransform().Value.rotation;
                Quaternion rotation = GetRotation(enemyPosition, characterPosition);
                
                if (characterRotation == rotation)
                    continue;
                
                if (entity.HasTargetRotation() == false)
                    entity.AddTargetRotation(rotation);

                entity.ReplaceTargetRotation(rotation);
            }
        }
        
        private Quaternion GetRotation(Vector3 enemyPosition, Vector3 characterPosition)
        {
            Vector3 lookDirection = enemyPosition - characterPosition;
            lookDirection.y = characterPosition.y;
            float angle = Vector3.SignedAngle(Vector3.forward, lookDirection, Vector3.up);

            return Quaternion.Euler(0, angle, 0);
        }
    }
}
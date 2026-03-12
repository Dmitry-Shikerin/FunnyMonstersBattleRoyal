using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Damage.Domain
{
    [Component(group: ComponentGroup.Player)]
    public struct ObstacleCollisionEvent
    {
        public ControllerColliderHit Hit;
        public ProtoEntity Obstacle;
        //public ObstacleDamageData DamageData;
    }
}
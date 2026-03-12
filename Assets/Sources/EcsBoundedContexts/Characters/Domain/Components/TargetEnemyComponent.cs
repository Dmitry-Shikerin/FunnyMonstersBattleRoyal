using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Characters.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring("TargetEnemy")]
    [Component(group: ComponentGroup.Characters)]
    public struct TargetEnemyComponent
    {
        public ProtoEntity Value;
    }
}
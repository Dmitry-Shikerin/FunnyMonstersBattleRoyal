using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine.AI;

namespace Sources.EcsBoundedContexts.Movements.Move.Components
{
    [Serializable] 
    [ProtoUnityAuthoring("NavMeshAgent")]
    [Component(group: ComponentGroup.Common)]
    public struct NavMeshComponent
    {
        public NavMeshAgent Value;
    }
}
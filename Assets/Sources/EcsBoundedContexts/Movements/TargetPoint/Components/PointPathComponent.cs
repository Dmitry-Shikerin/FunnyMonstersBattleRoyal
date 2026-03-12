using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Movements.TargetPoint.Components
{
    [Serializable] 
    [ProtoUnityAuthoring("MovementPoint")]
    [Component(group: ComponentGroup.Common)]
    public struct PointPathComponent
    {
        public Vector3[] Points;
    }
}
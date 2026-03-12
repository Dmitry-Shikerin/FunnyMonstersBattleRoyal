using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Types;

namespace Sources.EcsBoundedContexts.SwingingTrees.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Tree)]
    public struct SwingingTreeComponent
    {
        public TreeType TreeType;
        public bool EnableYAxisSwingingTree;
        public float SpeedX;
        public float SpeedY;
        public float MaxAngleX;
        public float MaxAngleY;
        public float Direction;
    }
}
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Types;
using UnityEngine;

namespace Sources.EcsBoundedContexts.SwingingTrees.Presentation
{
    public class TreeModule : EntityModule
    {
        [field: SerializeField] public TreeType TreeType { get; private set; }
    }
}
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Spawners.Presentation
{
    public class SpawnPointModule : EntityModule
    {
        [field: Required] [field: SerializeField] public Transform SpawnPointTransform { get; private set; }
    }
}
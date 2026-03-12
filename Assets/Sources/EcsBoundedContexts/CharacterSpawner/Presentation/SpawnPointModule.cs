using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation.Types;
using UnityEngine;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Presentation
{
    public class SpawnPointModule : EntityModule
    {
        [field: SerializeField] public SpawnPointType SpawnPointType { get; private set; }
    }
}
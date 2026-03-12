using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation;
using UnityEngine;

namespace Sources.EcsBoundedContexts.EnemySpawners.Presentation
{
    public class EnemySpawnPointModule : SpawnPointModule
    {
        [field: SerializeField] public EntityLink MeleeSpawnPoint { get; private set; }
        [field: SerializeField] public EntityLink RangeSpawnPoint { get; private set; }
    }
}
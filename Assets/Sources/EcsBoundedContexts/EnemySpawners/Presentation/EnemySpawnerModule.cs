using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.CharacterSpawner.Extensions;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation.Types;
using UnityEngine;

namespace Sources.EcsBoundedContexts.EnemySpawners.Presentation
{
    public class EnemySpawnerModule : EntityModule, ISelfValidator
    {
        [field: ChildGameObjectsOnly] [field: SerializeField]
        public List<EntityLink> SpawnPoints { get; private set; }

        public void Validate(SelfValidationResult result) =>
            SpawnPoints.ValidateSpawnPoints<EnemySpawnPointModule>(SpawnPointType.Enemy, result);

        [Button]
        private void AddEnemySpawnPoints() =>
            SpawnPoints = gameObject.GetSpawnPoints<EnemySpawnPointModule>(SpawnPointType.Enemy);
    }
}
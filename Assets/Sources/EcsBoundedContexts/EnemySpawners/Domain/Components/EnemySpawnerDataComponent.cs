using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Components
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Ability)]
    public struct EnemySpawnerDataComponent
    {
        public int SpawnedEnemies;
        public int SpawnedBosses;
        public int SpawnedKamikaze;
        public int WaweIndex;
        public EnemyType LastSpawnedEnemyType;
    }
}
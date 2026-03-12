using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Components
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Ability)]
    public struct EnemySpawnerConfigComponent
    {
        public EnemySpawnerConfig Value;
    }
}
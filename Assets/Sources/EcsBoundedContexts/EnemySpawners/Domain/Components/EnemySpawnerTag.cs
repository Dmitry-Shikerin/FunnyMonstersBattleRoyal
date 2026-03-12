using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Components
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Ability)]
    public struct EnemySpawnerTag
    {
    }
}
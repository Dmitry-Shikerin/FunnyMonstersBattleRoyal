using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation.Types;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Ability)]
    public struct CharacterSpawnPointTypeComponent
    {
        public SpawnPointType Value;
    }
}
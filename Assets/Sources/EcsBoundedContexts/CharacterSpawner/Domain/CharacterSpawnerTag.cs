using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Ability)]
    public struct CharacterSpawnerTag
    {
    }
}
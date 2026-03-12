using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Characters.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring("CharacterTag")]
    [Component(group: ComponentGroup.Characters)]
    public struct MassAttackEvent
    {
    }
}
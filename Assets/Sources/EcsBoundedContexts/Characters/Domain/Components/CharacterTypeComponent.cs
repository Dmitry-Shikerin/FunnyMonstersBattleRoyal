using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Characters.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Characters)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public struct CharacterTypeComponent
    {
        public CharacterType Value;
    }
}
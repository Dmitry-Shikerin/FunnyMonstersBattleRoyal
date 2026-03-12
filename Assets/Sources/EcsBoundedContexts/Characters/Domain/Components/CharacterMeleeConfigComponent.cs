using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Characters.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Characters)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public struct CharacterMeleeConfigComponent
    {
        public CharacterMeleeConfig Value;
    }
}
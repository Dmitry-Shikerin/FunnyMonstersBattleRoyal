using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Bunker.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Bunker.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public struct BunkerUiModuleComponent
    {
        public BunkerUiModule Value;
    }
}
using System;
using Animancer;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Animancers.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public struct AnimancerStateComponent
    {
        public AnimancerState Value;
    }
}
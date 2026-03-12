using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Animators
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public struct AnimatorComponent
    {
        public Animator Value;
    }
}
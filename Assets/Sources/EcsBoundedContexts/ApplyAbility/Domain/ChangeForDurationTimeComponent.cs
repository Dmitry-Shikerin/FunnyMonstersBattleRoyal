using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.ApplyAbility.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public struct ChangeForDurationTimeComponent
    {
        public float Duration;
        public float AnimationTime;
        public float TargetValue;
        public float FromValue;
        public float Value;
    }
}
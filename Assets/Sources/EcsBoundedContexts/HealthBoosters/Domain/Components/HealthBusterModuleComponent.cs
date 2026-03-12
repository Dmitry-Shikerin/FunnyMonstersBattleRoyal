using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.HealthBoosters.Presentation;

namespace Sources.EcsBoundedContexts.HealthBoosters.Domain.Components
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct HealthBusterModuleComponent
    {
        public HealthBusterModule Value;
    }
}
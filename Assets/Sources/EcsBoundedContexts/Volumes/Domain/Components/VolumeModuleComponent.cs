using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Volumes.Presentation;

namespace Sources.EcsBoundedContexts.Volumes.Domain.Components
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct VolumeModuleComponent
    {
        public VolumeModule Value;
    }
}
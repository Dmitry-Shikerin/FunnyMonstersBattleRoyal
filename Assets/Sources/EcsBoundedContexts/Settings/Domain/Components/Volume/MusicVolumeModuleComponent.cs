using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Settings.Presentation;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components.Volume
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct MusicVolumeModuleComponent
    {
        public MusicVolumeModule Value;
    }
}
using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components.Volume
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct MutedSoundVolumeComponent
    {
    }
}
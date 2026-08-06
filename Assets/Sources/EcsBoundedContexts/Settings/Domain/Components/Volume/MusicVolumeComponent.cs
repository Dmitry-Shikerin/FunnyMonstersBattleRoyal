using System;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components.Volume
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct MusicVolumeComponent
    {
        public float Value;
    }
}
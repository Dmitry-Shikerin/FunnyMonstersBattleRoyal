using System;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components.Parts
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct ResolutionComponent
    {
        public int Width;
        public int Height;
        public int RefreshRate;
    }
}
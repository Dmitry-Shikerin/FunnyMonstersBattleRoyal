using System;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components.Parts
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct FramerateComponent
    {
        public int Value;
    }
}
using System;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct GraphicsQualityComponent
    {
        public string Value;
    }
}
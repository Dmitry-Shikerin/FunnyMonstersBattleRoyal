using System;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Settings.Domain.Enums;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components.Parts
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct GraphicsQualityComponent
    {
        public GraphicsQualities Value;
    }
}
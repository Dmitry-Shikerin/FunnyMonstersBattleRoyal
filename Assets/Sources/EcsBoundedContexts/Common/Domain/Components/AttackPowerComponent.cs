using System;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Common.Domain.Components
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public struct AttackPowerComponent
    {
        public int Value;
    }
}
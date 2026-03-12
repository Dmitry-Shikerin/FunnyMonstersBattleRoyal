using System;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;

namespace Sources.EcsBoundedContexts.Enemies.Domain.Components
{
    [Serializable] 
    [Component(group: ComponentGroup.Enemy)]
    public struct EnemyTypeComponent
    {
        public EnemyType Value;
    }
}
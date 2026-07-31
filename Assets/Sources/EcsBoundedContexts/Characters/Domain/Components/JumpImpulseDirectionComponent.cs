using System;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Domain.Components
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct JumpImpulseDirectionComponent
    {
        public Vector3 Value;
    }
}
using System;
using Sources.EcsBoundedContexts.Core.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components.Parts
{
    [Serializable] 
    [Component(group: ComponentGroup.Characters)]
    public struct FullScreenModeComponent
    {
        public FullScreenMode Value;
    }
}
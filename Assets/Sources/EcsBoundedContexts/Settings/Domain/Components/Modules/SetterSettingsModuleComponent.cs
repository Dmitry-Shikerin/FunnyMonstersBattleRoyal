using System;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Settings.Presentation;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components.Modules
{
    [Serializable]
    [Component(group: ComponentGroup.Characters)]
    public struct SetterSettingsModuleComponent
    {
        public SetterSettingsModule Value;
    }
}
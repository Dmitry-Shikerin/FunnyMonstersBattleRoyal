using System;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Settings.Domain.Data;

namespace Sources.EcsBoundedContexts.Settings.Domain.Components
{
    [Serializable]
    [Component(group: ComponentGroup.Characters)]
    public struct SavedSettingsComponent
    {
        public SettingsSaveData Value;
    }
}
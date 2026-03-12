using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Upgrades.Presentation;

namespace Sources.EcsBoundedContexts.Upgrades.Domain.Components
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Upgrade)]
    public struct ApplyUpgradeUiModuleComponent
    {
        public ApplyUpgradeModule Value;
    }
}
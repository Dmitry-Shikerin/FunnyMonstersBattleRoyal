using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Upgrades.Domain.Configs;

namespace Sources.EcsBoundedContexts.Upgrades.Domain.Components
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Upgrade)]
    public struct UpgradeConfigComponent
    {
        public UpgradeConfig Value;
        public int Index;
    }
}
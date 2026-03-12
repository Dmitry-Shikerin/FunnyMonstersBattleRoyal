using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.DailyRewards.Presentation;

namespace Sources.EcsBoundedContexts.DailyRewards.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct DailyRewardModuleComponent
    {
        public DailyRewardModule Value;
    }
}
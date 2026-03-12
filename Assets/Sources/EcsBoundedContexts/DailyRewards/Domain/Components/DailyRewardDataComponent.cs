using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;

namespace Sources.EcsBoundedContexts.DailyRewards.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Common)]
    public struct DailyRewardDataComponent
    {
        public DateTime LastRewardTime;
        public TimeSpan CurrentTime;
        public DateTime TargetRewardTime;
        public DateTime ServerTime;
    }
}
using System;
using Sources.Frameworks.GameServices.Loads.Domain;

namespace Sources.EcsBoundedContexts.DailyRewards.Domain.Data
{
    public struct DailyRewardSaveData : IEntitySaveData
    {
        public string Id { get; set; }
        public DateTime LastRewardTime {get; set; }
        public TimeSpan CurrentTime { get; set; }
        public DateTime TargetRewardTime { get; set; }
        public DateTime ServerTime { get; set; }
    }
}
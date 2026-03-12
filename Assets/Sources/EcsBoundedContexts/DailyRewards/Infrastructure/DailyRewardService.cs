using System;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Components;

namespace Sources.EcsBoundedContexts.DailyRewards.Infrastructure
{
    public class DailyRewardService
    {
        private readonly TimeSpan _oneSecond = TimeSpan.FromSeconds(1);
        
        public bool TrySetTargetRewardTime(ProtoEntity entity)
        {
            if (IsAvailable(entity) == false)
                return false;
            
            ref DailyRewardDataComponent data = ref entity.GetDailyRewardData();
            data.LastRewardTime = data.ServerTime;
            data.TargetRewardTime = data.LastRewardTime + TimeSpan.FromDays(1);
            return true;
        }
        
        public void SetServerTime(ProtoEntity entity, DateTime serverTime)
        {
            ref DailyRewardDataComponent data = ref entity.GetDailyRewardData();
            data.ServerTime = serverTime;
        }

        public void IncreaseServerTime(ProtoEntity entity)
        {
            ref DailyRewardDataComponent data = ref entity.GetDailyRewardData();
            data.ServerTime += _oneSecond;
        }
        
        public void SetCurrentTime(ProtoEntity entity)
        {
            ref DailyRewardDataComponent data = ref entity.GetDailyRewardData();
            data.CurrentTime = data.TargetRewardTime - data.ServerTime;
        }
        
        public string GetTimerText(ProtoEntity entity)
        {
            DailyRewardDataComponent data = entity.GetDailyRewardData();
            TimeSpan currentTime = data.CurrentTime;
            
            if (IsAvailable(entity))
                return "00:00:00";
        
            string hours = currentTime.Hours < 10 ? "0" + currentTime.Hours : currentTime.Hours.ToString();
            string minutes = currentTime.Minutes < 10 ? "0" + currentTime.Minutes : currentTime.Minutes.ToString();
            string seconds = currentTime.Seconds < 10 ? "0" + currentTime.Seconds : currentTime.Seconds.ToString();
            
            return $"{hours}:{minutes}:{seconds}";
        }

        public bool IsAvailable(ProtoEntity entity)
        {
            DailyRewardDataComponent data = entity.GetDailyRewardData();
            
            return data.CurrentTime <= TimeSpan.Zero;
        }
    }
}
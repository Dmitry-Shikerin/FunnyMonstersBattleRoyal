using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Timers.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Timers.Infrastructure
{
    [EcsSystem(79)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class TimerSystem : IProtoRunSystem
    {
        [DI] private readonly GameAspect _aspect;
        [DI] private readonly ProtoIt _it = new (It.Inc<TimerComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ref TimerComponent timer = ref entity.GetTimer();
                
                timer.Value -= Time.deltaTime;

                if (timer.Value > 0)
                    continue;
                
                entity.DelTimer();
            }
        }
    }
}
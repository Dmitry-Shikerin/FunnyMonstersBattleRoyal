using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.BurnAbilities.Domain.Components;
using Sources.EcsBoundedContexts.BurnAbilities.Domain.Configs;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.BurnAbilities.Controllers
{
    [EcsSystem(50)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class BurnSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                BurnParticleComponent,
                BurnEvent>());
        [DI] private readonly ProtoIt _timerIt = new(
            It.Inc<
                BurnParticleComponent,
                BurnTimerComponent,
                BurnPeriodicTimerComponent>());
        [DI] private readonly ProtoIt _forbiddingUseBurnTimerIt = new(
            It.Inc<
                ForbiddingUseBurnTimerComponent>());
        
        private readonly IAssetCollector _assetCollector;
        private BurnConfig _config;

        public BurnSystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        public void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<BurnConfig>();
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                if (entity.HasForbiddingUseBurnTimer())
                    continue;
                
                //TODO вынести значения в конфиг
                if (entity.HasBurnTimer())
                {
                    if (entity.HasDamageEvent())
                    {
                        ref var damageComponent = ref entity.GetDamageEvent();
                        damageComponent.Value += _config.InstantDamage;
                    }
                    else
                    {
                        entity.AddDamageEvent(_config.InstantDamage);
                    }
                    
                    entity.ReplaceBurnTimer(_config.BurnDuration);
                    continue;
                }
                
                entity.AddForbiddingUseBurnTimer(_config.ForbiddingUseDelay); //таймер для запрета накидывания берна
                entity.AddBurnTimer(_config.BurnDuration);
                entity.AddBurnPeriodicTimer(_config.BurnTickDelay);
                entity.GetBurnParticle().Value.Play();
            }

            foreach (ProtoEntity entity in _timerIt)
            {
                ref BurnTimerComponent burnTimerComponent = ref entity.GetBurnTimer();
                burnTimerComponent.Value -= Time.deltaTime;
                ref BurnPeriodicTimerComponent burnPeriodicTimerComponent = ref entity.GetBurnPeriodicTimer();
                burnPeriodicTimerComponent.Value -= Time.deltaTime;

                if (burnTimerComponent.Value <= 0)
                {
                    entity.DelBurnTimer();
                    entity.DelBurnPeriodicTimer();
                    entity.GetBurnParticle().Value.Stop();

                    continue;
                }

                if (burnPeriodicTimerComponent.Value <= 0)
                {
                    entity.ReplaceBurnPeriodicTimer(_config.BurnTickDelay);
                    
                    if (entity.HasDamageEvent())
                    {
                        ref var damageComponent = ref entity.GetDamageEvent();
                        damageComponent.Value += _config.TickDamage;
                    }
                    else
                    {
                        entity.AddDamageEvent(_config.TickDamage);
                    }
                }
            }

            foreach (ProtoEntity entity in _forbiddingUseBurnTimerIt)
            {
                ref ForbiddingUseBurnTimerComponent forbiddingUseBurnTimerComponent = ref entity.GetForbiddingUseBurnTimer();
                forbiddingUseBurnTimerComponent.Value -= Time.deltaTime;

                if (forbiddingUseBurnTimerComponent.Value > 0)
                    continue;
                
                entity.DelForbiddingUseBurnTimer();
            }
        }
    }
}
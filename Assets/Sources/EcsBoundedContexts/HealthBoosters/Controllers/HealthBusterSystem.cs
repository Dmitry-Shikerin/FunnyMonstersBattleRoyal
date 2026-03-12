using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Bunker.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.HealthBoosters.Domain.Components;
using Sources.EcsBoundedContexts.HealthBoosters.Presentation;

namespace Sources.EcsBoundedContexts.HealthBoosters.Controllers
{
    [EcsSystem(69)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public class HealthBusterSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _decreaseIt = new(
            It.Inc<
                HealthBusterComponent,
                DecreaseEvent>());        
        [DI] private readonly ProtoIt _increaseIt = new(
            It.Inc<
                HealthBusterComponent,
                IncreaseHealthBoosterEvent>());
        [DI] private readonly ProtoIt _bunkerIt = new(
            It.Inc<BunkerTag>());
        [DI] private readonly ProtoIt _initIt = new(
            It.Inc<
                HealthBusterComponent,
                InitializeEvent>());

        public void Init(IProtoSystems systems)
        {
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _initIt)
            {
                UpdateView(entity);
            }
            
            foreach (ProtoEntity entity in _decreaseIt)
            {
                ref HealthBusterComponent healthBuster = ref entity.GetHealthBuster();

                if (healthBuster.Value <= 0)
                    continue;

                healthBuster.Value--;
                
                if (entity.HasFirstUsedCompleted() == false)
                    entity.AddFirstUsedCompleted();
                
                foreach (ProtoEntity bunker in _bunkerIt)
                    bunker.AddIncreaseEvent();
                
                UpdateView(entity);
            }        
            
            foreach (ProtoEntity entity in _increaseIt)
            {
                ref HealthBusterComponent healthBuster = ref entity.GetHealthBuster();
                int value = entity.GetIncreaseHealthBoosterEvent().Value;
                
                //TODO обобщить с дейли ревард там есть эта логика
                healthBuster.Value += value;
                //Debug.Log($"Increase health buster: {healthBuster.Value}");
                
                UpdateView(entity);
            }
        }

        private void UpdateView(ProtoEntity entity)
        {
            ref HealthBusterComponent healthBuster = ref entity.GetHealthBuster();
            HealthBusterModule module = entity.GetHealthBusterModule().Value;
            module.Text.text = healthBuster.Value.ToString();

            if (healthBuster.Value > 0)
            {
                //TODO костыль
                if (module.Button == null)
                    return;
                
                module.Button.interactable = true;
                return;
            }
            
            //TODO костыль
            if (module.Button == null)
                return;
            
            module.Button.interactable = false;
        }
    }
}
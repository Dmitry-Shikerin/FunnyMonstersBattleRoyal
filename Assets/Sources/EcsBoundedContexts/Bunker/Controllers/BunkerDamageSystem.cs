using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Bunker.Domain.Components;
using Sources.EcsBoundedContexts.Bunker.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Damage.Domain;

namespace Sources.EcsBoundedContexts.Bunker.Controllers
{
    [EcsSystem(70)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class BunkerDamageSystem : IProtoRunSystem, IProtoInitSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                BunkerTag,
                DamageEvent>());          
        [DI] private readonly ProtoIt _initIt = new(
            It.Inc<
                BunkerTag>());        
        [DI] private readonly ProtoIt _increaseIt = new(
            It.Inc<
                BunkerTag,
                IncreaseEvent>());

        public void Init(IProtoSystems systems)
        {
            foreach (ProtoEntity entity in _initIt)
                UpdateHealthText(entity);
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                ref HealthComponent health = ref entity.GetHealth();
                health.Value--;
                
                BunkerUiModule module = entity.GetBunkerUiModule().Value;
                UpdateHealthText(entity);
                module.DamageVignetteAnimation.Restart();
                module.HealthAnimation.Restart();
            }
            
            foreach (ProtoEntity entity in _increaseIt)
            {
                ref HealthComponent health = ref entity.GetHealth();
                health.Value++;
                
                BunkerUiModule module = entity.GetBunkerUiModule().Value;
                UpdateHealthText(entity);
                module.HealVignetteAnimation.Restart();
                module.HealthAnimation.Restart();
            }
        }

        private void UpdateHealthText(ProtoEntity entity)
        {
            BunkerUiModule module = entity.GetBunkerUiModule().Value;
            module.HealthText.text = entity.GetHealth().Value.ToString();
        }
        
        // public void TakeDamage(IEnemyViewBase enemyView)
        // {
        //     _bunker.TakeDamage();
        //     _view.DamageAnimator.Play();
        //     enemyView.Destroy();
        //     ShowHighlight();
        //     _soundyService.Play("Sounds", "Bunker");
        // }
        //
        // private async void ShowHighlight()
        // {
        //     _tokenSource.Cancel();
        //     _tokenSource = new CancellationTokenSource();
        //     HighlightEffect highlight = _view.HighlightEffect;
        //     float highlightDelta = _view.HighlightDelta;
        //     
        //     try
        //     {
        //         highlight.glow = 5f;
        //         highlight.overlay = 1f;
        //
        //         while (highlight.glow > 0f 
        //                && highlight.overlay > 0f 
        //                && _tokenSource.Token.IsCancellationRequested == false)
        //         {
        //             highlight.glow = Mathf.MoveTowards(
        //                 highlight.glow, 0, highlightDelta * 5 * Time.deltaTime);
        //             highlight.overlay = Mathf.MoveTowards(
        //                 highlight.overlay, 0, highlightDelta * Time.deltaTime);
        //             
        //             await UniTask.Yield(_tokenSource.Token);
        //         }
        //     }
        //     catch (OperationCanceledException)
        //     {
        //     }
        // }
    }
}
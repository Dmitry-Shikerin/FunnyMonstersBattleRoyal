using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Bunker.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Damage.Domain;
using Sources.Frameworks.DeepFramework.DeepUtils.Constants;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using TMPro;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Damage.Controllers
{
    [EcsSystem(64)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class DamageSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoItExc _it = new(
            It.Inc<
                HealthComponent,
                DamageEvent>(), 
            It.Exc<
                BunkerTag>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                int damage = entity.GetDamageEvent().Value;

                ref HealthComponent healthComponent = ref entity.GetHealth();
                healthComponent.Value -= damage;
                ChangeHealthBar(entity);
                ChangeHealthText(entity);
                PlayBloodParticle(entity);
            }
        }
        
        private void PlayBloodParticle(ProtoEntity entity)
        {
            if (entity.HasBloodParticle() == false)
                return;
            
            entity.GetBloodParticle().Value.Play();
        }

        private void ChangeHealthText(ProtoEntity entity)
        {
            if (entity.HasHealthText() == false)
                return;
            
            int health = entity.GetHealth().Value;
            TMP_Text text = entity.GetHealthText().Value;
            text.text = health.ToString();
        }
        
        private void ChangeHealthBar(ProtoEntity entity)
        {
            if (entity.HasHealthBar() == false)
                return;
            
            int maxHealth = entity.GetMaxHealth().Value;
            int health = entity.GetHealth().Value;
            Image healthBar = entity.GetHealthBar().Value;
            float percent = health.IntToPercent(maxHealth);
            healthBar.fillAmount = percent * MathConst.UnitMultiplier;
        }
    }
}
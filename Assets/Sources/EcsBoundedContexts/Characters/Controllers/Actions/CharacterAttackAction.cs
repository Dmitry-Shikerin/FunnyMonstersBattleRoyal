using System.ComponentModel;
using Leopotam.EcsProto;
using NodeCanvas.Framework;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Characters.Domain.Enums;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [ParadoxNotion.Design.Category(NcCategoriesConst.Characters)]
    public class CharacterAttackAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;
        
        protected override void OnExecute()
        {
            switch (_entity.GetCharacterType().Value)
            {
                case CharacterType.Melee:
                    MeleeAttack();
                    break;
                case CharacterType.Range:
                    RangeAttack();
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private void RangeAttack()
        {
            _entity
                .PlayAnimation(AnimationName.ShootSecondaryAssaultRifle)
                .SetCallback(AnimationEventName.Attack, OnRangeAttack);
        }

        private void OnRangeAttack()
        {
            if (_entity.HasTargetEnemy() == false)
            {
                EndAction();
                return;
            }
            
            _entity.GetShootParticle().Value.Play();

            ProtoEntity enemy = _entity.GetTargetEnemy().Value;
            int attack = _entity.GetAttackPower().Value;

            if (enemy.HasDamageEvent())
            {
                ref var damageComponent = ref enemy.GetDamageEvent();
                damageComponent.Value += attack;
                
                return;
            }
            
            enemy.AddDamageEvent(attack);
        }

        private void MeleeAttack()
        {
            _entity
                .PlayAnimation(AnimationName.SwordAttack)
                .SetCallback(AnimationEventName.Attack, OnMeleeAttack);
        }

        private void OnMeleeAttack() =>
            _entity.AddMassAttackEvent();
    }
}
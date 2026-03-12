using System.ComponentModel;
using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using NodeCanvas.Framework;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Enemies.Domain.Configs;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.EcsBoundedContexts.Enemies.Controllers.Actions
{
    [ParadoxNotion.Design.Category(NcCategoriesConst.Enemies)]
    public class EnemyAttackAction : ActionTask
    {
        private ProtoEntity _entity;
        private EnemyBossConfig _config;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;   
        
        [Inject]
        private void Construct(IAssetCollector assetCollector) =>
            _config = assetCollector.Get<EnemyBossConfig>();

        protected override void OnExecute()
        {
            if (_entity.HasGunOwnerModule())
                _entity.GetGunOwnerModule().Value.Equip();
            
            
            switch (_entity.GetEnemyType().Value)
            {
                case EnemyType.Boss:
                    EnemyAttack();
                    _entity.AddMassAttackTimer(_config.MassAttackDelay);
                    break;
                case EnemyType.Enemy:
                    EnemyAttack();
                    break;
                case EnemyType.Default:
                    throw new InvalidEnumArgumentException();
            }
        }

        protected override void OnUpdate()
        {
        }

        protected override void OnStop()
        {
            if (_entity.HasMassAttackTimer())
                _entity.DelMassAttackTimer();
            
            if (_entity.HasGunOwnerModule())
                _entity.GetGunOwnerModule().Value.UnEquip();
        }
        
        private void EnemyAttack()
        {
            _entity
                .PlayAnimation(AnimationName.SwordAttack)
                .SetCallback(AnimationEventName.Attack, OnEnemyAttack);
        }

        private void OnEnemyAttack()
        {
            // if (_entity.HasTargetCharacter() == false)
            // {
            //     EndAction();
            //     return;
            // }
            
            ProtoEntity entity = _entity.GetTargetCharacter().Value;
            int damage = _entity.GetAttackPower().Value;
            
            if (entity.HasDamageEvent())
            {
                entity.GetDamageEvent().Value += damage;
                return;
            }
            
            entity.AddDamageEvent(damage);
        }
    }
}
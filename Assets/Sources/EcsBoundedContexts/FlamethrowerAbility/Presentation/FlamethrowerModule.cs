using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Core;
using UnityEngine;

namespace Sources.EcsBoundedContexts.FlamethrowerAbility.Presentation
{
    public class FlamethrowerModule : EntityModule
    {
        [SerializeField] private EntityLinkParticleCollision _particleCollision;

        [field: Required]
        [field: SerializeField]
        public ParticleSystem FlameParticle { get; private set; }

        private void OnEnable()
        {
            _particleCollision.Entered += OnEntered;
        }

        protected override void OnAfterDisable()
        {
            _particleCollision.Entered -= OnEntered;
        }

        private void OnEntered(EntityLink link)
        {
            ProtoEntity entity = link.Entity;

            if (entity.HasBurnParticle() == false)
                return;

            if (entity.HasBurnEvent())
                return;
            
            entity.AddBurnEvent();
        }
    }
}
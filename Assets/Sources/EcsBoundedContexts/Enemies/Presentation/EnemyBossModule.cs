using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Presentation
{
    public class EnemyBossModule : EnemyModule
    {
        [field: Required]
        [field: SerializeField]
        public ParticleSystem MassAttackParticle { get; private set; }
    }
}
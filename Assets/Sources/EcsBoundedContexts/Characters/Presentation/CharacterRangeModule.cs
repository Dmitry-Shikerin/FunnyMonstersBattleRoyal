using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation
{
    public class CharacterRangeModule : CharacterModule
    {
        [field: SerializeField] public ParticleSystem ShootParticle { get; private set; }
    }
}
using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NukeAbilities.Presentation
{
    public class NukeAbilityModule : EntityModule
    {
        [field: Required] [field: SerializeField] public List<Transform> Path { get; private set; }
        [field: Required] [field: SerializeField] public ParticleSystem NukeParticle { get; private set; }
        [field: Required] [field: SerializeField] public BoxCollider DamageCollider { get; private set; }
    }
}
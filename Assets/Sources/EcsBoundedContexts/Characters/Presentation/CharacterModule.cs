using Animancer;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using NodeCanvas.BehaviourTrees;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Characters.Presentation
{
    public abstract class CharacterModule : EntityModule
    {
        [Required] [field: SerializeField] public BehaviourTreeOwner BehaviourTreeOwner { get; private set; }
        [Required] [field: SerializeField] public Transform HealthBarTransform { get; private set; }
        [Required] [field: SerializeField] public Image HealthBarImage { get; private set; }
        [Required] [field: SerializeField] public ParticleSystem HealthParticle { get; private set; }
        [Required] [field: SerializeField] public AnimancerComponent Animancer { get; private set; }
    }
}
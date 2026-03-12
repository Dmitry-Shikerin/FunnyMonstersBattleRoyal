using Animancer;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.StateMachines;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Enemies.Presentation
{
    public class EnemyModule : EntityModule
    {
        [field: SerializeField] public BehaviourTreeOwner BehaviourTreeOwner { get; private set; }    
        [field: SerializeField] public Transform Transform { get; private set; }        
        [field: SerializeField] public Transform HealthBarTransform { get; private set; }
        [field: SerializeField] public ParticleSystem BurnParticleSystem { get; private set; }
        [field: SerializeField] public Image HealthBarImage { get; private set; }
        [field: SerializeField] public ParticleSystem BloodParticleSystem { get; private set; }
        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }
        [Required] [field: SerializeField] public AnimancerComponent Animancer { get; private set; }
    }
}
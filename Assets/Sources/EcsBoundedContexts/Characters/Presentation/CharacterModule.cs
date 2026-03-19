using Animancer;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using NodeCanvas.StateMachines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation
{
    public class CharacterModule : EntityModule
    {
        [Required] [field: SerializeField] public FSMOwner FsmOwner { get; private set; }
        [Required] [field: SerializeField] public AnimancerComponent Animancer { get; private set; }
        [Required] [field: SerializeField] public CharacterController CharacterController { get; private set; }
    }
}
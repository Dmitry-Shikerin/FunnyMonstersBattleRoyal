using Fusion;
using NodeCanvas.StateMachines;
using Reflex.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation
{
    public class CharacterMovementViewComponent : NetworkBehaviour, IViewComponent
    {
        [field: Required] [field: SerializeField] public FSMOwner FsmOwner { get; private set; }
        [field: Required] [field: SerializeField] public Transform GroundCheckTransform { get; private set; }
        [field: Required] [field: SerializeField] public CharacterController CharacterController { get; private set; }

        [Networked]
        public NetworkBool IsGrounded { get; set; }
        [Networked]
        public float GroundDistance { get; set; }
        [Networked]
        public Vector3 CharacterDirection { get; set; }        
        [Networked]
        public Vector3 JumpImpulseDirection { get; set; }
        [Networked]
        public float CharacterSpeed { get; set; }
        [Networked]
        public NetworkBool IsJumping { get; set; }
        [Networked]
        public float JumpTimer { get; set; }
        [Networked]
        public float Gravity { get; set; }        
        [Networked]
        public NetworkBool IsAir { get; set; }

        private CharacterConfig _characterConfig;
        
        public PlayerRef PlayerRef { get; private set; }

        [Inject]
        public void Construct(IAssetCollector assetCollector)
        {
            
        }
        
        public void Init(PlayerRef playerRef) =>
            PlayerRef = playerRef;

        public override void FixedUpdateNetwork()
        {
            if (Runner.IsClient)
                return;

            if (FsmOwner.isRunning == false)
                return;
            
            FsmOwner.UpdateBehaviour();
        }
    }
}
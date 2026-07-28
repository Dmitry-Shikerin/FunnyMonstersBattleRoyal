using Fusion;
using Sources.BoundedContexts.Networks.Input;
using Sources.BoundedContexts.TestGameplays;
using UnityEngine;

namespace Sources.BoundedContexts.Networks
{
    public class NetworkMovementController : NetworkBehaviour
    {
        [SerializeField] private MovementComponent _movementComponent;
        [SerializeField] private NetworkInputReceiver _networkInputReceiver;

        public override void FixedUpdateNetwork()
        {
            _movementComponent.Move(_networkInputReceiver.MovementDirection, Runner.DeltaTime);
        }
    }
}
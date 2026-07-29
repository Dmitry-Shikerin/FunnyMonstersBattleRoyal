using Fusion;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Input.Domain.Data.Network
{
    public struct NetworkInputData : INetworkInput
    {
        public Vector2 MovementInput;
        public Vector3 CameraForward;
        public NetworkButtons InputButtons;
    }
}
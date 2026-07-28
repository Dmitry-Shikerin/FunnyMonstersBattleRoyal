using Fusion;
using UnityEngine;

namespace Sources.BoundedContexts.Networks.Core
{
    public enum InputButtons
    {
        Jump = 0,
    }
    
    public struct NetworkInputData : INetworkInput
    {
        public Vector2 MovementInput;
        public NetworkButtons InputButtons;
    }
}
using Sources.Frameworks.StateMachines.ContextStateMachines.Interfaces.Contexts;
using UnityEngine;

namespace Sources.Frameworks.GameServices.InputServices.Inputs
{
    public class InputData : IContext
    {
        public Vector3 MoveDirection { get; set; }
        public Vector3 LookPosition { get; set; }
        public Vector3 PointerPosition { get; set; }
    }
}
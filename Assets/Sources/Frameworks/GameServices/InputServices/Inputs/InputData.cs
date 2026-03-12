using System;
using Sources.Frameworks.StateMachines.ContextStateMachines.Interfaces.Contexts;
using UnityEngine;

namespace Sources.Frameworks.GameServices.InputServices.Inputs
{
    public class InputData : IContext
    {
        public event Action StandChanged;
            
        public Vector3 MoveDirection { get; set; }
        public Vector3 LookPosition { get; set; }
        public Vector3 PointerPosition { get; set; }
        public float Speed { get; set; }
        public bool IsAttacking { get; set; }

        public void InvokeStand() =>
            StandChanged?.Invoke();
    }
}
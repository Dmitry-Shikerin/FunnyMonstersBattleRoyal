using System;
using UnityEngine;

namespace Sources.BoundedContexts.TestGameplays
{
    public class MovementInput : MonoBehaviour
    {
        private InputSystem_Actions _inputActions;

        private void Awake()
        {
            _inputActions = new InputSystem_Actions();
            _inputActions.Enable();
        }

        private void OnDestroy() =>
            _inputActions.Disable();

        public Vector2 GetMovementInput() =>
            _inputActions.Player.Move.ReadValue<Vector2>();
    }
}
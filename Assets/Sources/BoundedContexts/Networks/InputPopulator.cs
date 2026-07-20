using System;
using Fusion;
using Sources.BoundedContexts.TestGameplays;
using UnityEngine;

namespace Sources.BoundedContexts.Networks
{
    public class InputPopulator : MonoBehaviour
    {
        [SerializeField] private MovementInput _movementInput;
        [SerializeField] private NetworkCallbacksReceiver _callbacksReceiver;

        private void OnEnable()
        {
            _callbacksReceiver.OnPopulateInput += PopulateInput;
        }

        private void OnDisable()
        {
            _callbacksReceiver.OnPopulateInput -= PopulateInput;
        }

        private void PopulateInput(NetworkRunner runner, NetworkInput input)
        {
            input.Set(new NetworkInputData { MovementInput = _movementInput.GetMovementInput() });
        }
    }
}
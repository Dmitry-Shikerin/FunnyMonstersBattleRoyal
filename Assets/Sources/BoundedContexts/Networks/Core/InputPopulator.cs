using Fusion;
using Reflex.Attributes;
using Sources.BoundedContexts.TestGameplays;
using UnityEngine;

namespace Sources.BoundedContexts.Networks.Core
{
    public class InputPopulator : MonoBehaviour
    {
        private MovementInput _movementInput;
        private NetworkCallbacksReceiver _callbacksReceiver;

        private void OnEnable()
        {
            if (_callbacksReceiver == null)
                return;
            
            _callbacksReceiver.OnPopulateInput += PopulateInput;
        }

        private void OnDisable()
        {
            if (_callbacksReceiver == null)
                return;
            
            _callbacksReceiver.OnPopulateInput -= PopulateInput;
        }

        private void PopulateInput(NetworkRunner runner, NetworkInput input)
        {
            input.Set(new NetworkInputData { MovementInput = _movementInput.GetMovementInput() });
        }

        [Inject]
        private void Construct(NetworkCallbacksReceiver networkCallbacksReceiver, MovementInput input)
        {
            gameObject.SetActive(false);
            _movementInput = input;
            _callbacksReceiver = networkCallbacksReceiver;
            gameObject.SetActive(true);
        }
    }
}
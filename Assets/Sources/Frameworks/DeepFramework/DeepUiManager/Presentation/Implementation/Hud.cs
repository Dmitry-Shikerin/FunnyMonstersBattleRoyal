using NodeCanvas.StateMachines;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation
{
    public class Hud : MonoBehaviour
    {
        [field: SerializeField] public Canvas Canvas { get; private set; }
        [field: SerializeField] public FSMOwner FsmOwner { get; private set; }
        [field: SerializeField] public Camera UiCamera { get; private set; }
    }
}
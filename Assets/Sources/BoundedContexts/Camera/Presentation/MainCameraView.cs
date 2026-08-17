using Unity.Cinemachine;
using UnityEngine;

namespace Sources.BoundedContexts.Camera.Presentation
{
    public class MainCameraView : MonoBehaviour
    {
        [field: SerializeField] public UnityEngine.Camera Camera { get; private set; }
        [field: SerializeField] public CinemachineBrain Brain { get; private set; }
        [field: SerializeField] public ThirdPersonCameraView ThirdPersonCamera { get; private set; }
    }
}
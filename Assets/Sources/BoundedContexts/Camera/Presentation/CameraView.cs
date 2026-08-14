using Sources.EcsBoundedContexts.Cameras.Domain;
using Unity.Cinemachine;
using UnityEngine;

namespace Sources.BoundedContexts.Camera.Presentation
{
    public class CameraView : MonoBehaviour
    {
        [field: SerializeField] public UnityEngine.Camera Camera { get; private set; }
        [field: SerializeField] public CinemachineBrain Brain { get; private set; }
        [field: SerializeField] public CinemachineCamerasDictionary Cameras { get; private set; }

        public void SetFollow(VirtualCameraType cameraType, Transform followTransform) =>
            Cameras[cameraType].Follow = followTransform;
    }
}
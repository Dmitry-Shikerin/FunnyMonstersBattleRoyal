using System.Collections.Generic;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Unity.Cinemachine;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Cameras.Infrastructure.Services
{
    public interface ICameraService
    {
        void Initialize(
            IReadOnlyDictionary<VirtualCameraType, CinemachineCamera> cameras,
            VirtualCameraType firstCamera,
            Transform follower);
        void SetFollower(VirtualCameraType type, Transform follow);
        void HideAllCameras();
        bool IsSowed(VirtualCameraType type);
        bool TryShowCamera(VirtualCameraType type);
    }
}
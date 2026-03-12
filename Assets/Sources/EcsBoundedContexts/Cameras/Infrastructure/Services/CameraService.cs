using System;
using System.Collections.Generic;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Unity.Cinemachine;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Cameras.Infrastructure.Services
{
    public class CameraService : ICameraService
    {
        private IReadOnlyDictionary<VirtualCameraType, CinemachineCamera> _cameras;
        
        public VirtualCameraType ActiveCamera { get; private set; }

        public void Initialize(
            IReadOnlyDictionary<VirtualCameraType, CinemachineCamera> cameras, 
            VirtualCameraType firstCamera,
            Transform follower)
        {
            _cameras = cameras ?? throw new ArgumentNullException(nameof(cameras));

            foreach (KeyValuePair<VirtualCameraType, CinemachineCamera> camera in cameras)
            {
                if (camera.Value != null)
                    continue;
                
                throw new NullReferenceException($"Camera {camera.Key} not found");
            }
            
            HideAllCameras();
            SetFollowerForAll(follower);
            InitFirstCamera(firstCamera);
        }

        private void SetFollowerForAll(Transform follow)
        {
            foreach (KeyValuePair<VirtualCameraType, CinemachineCamera> camera in _cameras)
                camera.Value.Follow = follow;
        }

        public void SetFollower(VirtualCameraType type, Transform follow)
        {
            if (_cameras.TryGetValue(type, out CinemachineCamera camera) == false)
                throw new KeyNotFoundException($"Camera {type} not found");
            
            camera.Follow = follow;
        }

        public void HideAllCameras()
        {
            foreach (KeyValuePair<VirtualCameraType, CinemachineCamera> camera in _cameras)
                camera.Value.gameObject.SetActive(false);

            ActiveCamera = VirtualCameraType.Default;
        }

        public bool IsSowed(VirtualCameraType type) =>
            ActiveCamera == type;

        public bool TryShowCamera(VirtualCameraType type)
        {
            if (_cameras.TryGetValue(type, out CinemachineCamera camera) == false)
                throw new KeyNotFoundException($"Camera {type} not found");

            if (ActiveCamera == type)
                return false;
            
            if (_cameras.TryGetValue(ActiveCamera, out CinemachineCamera previousCamera) == false)
                throw new KeyNotFoundException($"Camera {type} not found");
            
            previousCamera.gameObject.SetActive(false);
            camera.gameObject.SetActive(true);
            ActiveCamera = type;

            return true;
        }

        private void InitFirstCamera(VirtualCameraType type)
        {
            if (_cameras.TryGetValue(type, out CinemachineCamera camera) == false)
                throw new KeyNotFoundException($"Camera {type} not found");
            
            camera.gameObject.SetActive(true);
            ActiveCamera = type;
        }
    }
}
using System;
using System.Collections.Generic;
using Sources.BoundedContexts.Camera.Presentation;
using Sources.BoundedContexts.Spawners.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.RootGameObjects.Presentation
{
    public class RootGameObject : MonoBehaviour
    {
        private const string CameraFolder = "Camera";
        private const string SpawnerFolder = "Spawner";
        
        [field: Header(CameraFolder)]
        [field: SerializeField] public MainCameraView MainCamera { get; private set; }
        
        [field: Header(SpawnerFolder)]
        [field: SerializeField] public List<SpawnPointView> SpawnPoints { get; private set; }

        public SpawnPointView GetAvailableSpawnPoint()
        {
            foreach (SpawnPointView spawnPoint in SpawnPoints)
            {
                if (spawnPoint == null)
                    throw new NullReferenceException();

                if (spawnPoint.IsBusy)
                    continue;

                return spawnPoint;
            }

            throw new InvalidOperationException();
        }
    }
}
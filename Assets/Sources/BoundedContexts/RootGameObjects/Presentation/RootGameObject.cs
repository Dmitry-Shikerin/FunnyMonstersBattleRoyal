using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.BoundedContexts.RootGameObjects.Presentation
{
    public class RootGameObject : MonoBehaviour
    {
        private const string CameraFolder = "Camera";
        private const string SpawnerFolder = "Spawner";
        private const string BunkerFolder = "Bunker";
        private const string AbilityFolder = "Spawner";
        private const string GrasFolder = "Gras";
        
        [field: FoldoutGroup(CameraFolder)]
        [field: SerializeField] public Camera MainCamera { get; private set; }
    }
}
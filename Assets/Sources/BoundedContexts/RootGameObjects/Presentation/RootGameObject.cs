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
        
        [field: FoldoutGroup(CameraFolder)]
        [field: SerializeField] public EntityLink MainCamera { get; private set; }
        
        [field: FoldoutGroup("Character")]
        [field: SerializeField] public Transform CharacterSpawnPoint { get; private set; }
        [field: SerializeField] public List<EntityLink> CharacterSpawnPoints { get; private set; }
    }
}
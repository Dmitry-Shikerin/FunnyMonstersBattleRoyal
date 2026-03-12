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
        
        [field: FoldoutGroup(SpawnerFolder)]
        [field: Required] [field: SerializeField] public EntityLink EnemySpawner { get; private set; }
        
        [field: FoldoutGroup(BunkerFolder)] 
        [field: Required] [field: SerializeField] public EntityLink Bunker { get; private set; }
        
        [field: FoldoutGroup(AbilityFolder)]
        [field: Required] [field: SerializeField] public EntityLink CharacterSpawner { get; private set; }
        [field: FoldoutGroup(AbilityFolder)]
        [field: Required] [field: SerializeField] public EntityLink FlamethrowerAbility { get; private set; }
        [field: FoldoutGroup(AbilityFolder)]
        [field: Required] [field: SerializeField] public EntityLink NukeAbility { get; private set; }        
        [field: FoldoutGroup(GrasFolder)]
        [field: Required] [field: SerializeField] public List<EntityLink> Gras { get; private set; }

    }
}
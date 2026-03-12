using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation.Types;
using UnityEngine;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Extensions
{
    public static class SpawnPointExtension
    {
        public static List<EntityLink> GetSpawnPoints<T>(this GameObject gameObject, SpawnPointType type) 
            where T : SpawnPointModule
        {
            return gameObject.GetComponentsInChildren<EntityLink>()
                .Where(spawnPoint => spawnPoint.TryGetModule(out T module) && module.SpawnPointType == type)
                .ToList();
        }
        
        public static void ValidateSpawnPoints<T>(this 
            List<EntityLink> spawnPoints,
            SpawnPointType spawnPointType, 
            SelfValidationResult result) 
            where T : SpawnPointModule
        {
            if (spawnPoints.Count == 0)
                result.AddError($"SpawnPoint type {spawnPointType} contains no SpawnPoints");
            
            foreach (EntityLink spawnPoint in spawnPoints)
            {
                T spawnPointModule = spawnPoint.GetModule<T>();
                
                if(spawnPointModule.SpawnPointType != spawnPointType)
                    result.AddError($"SpawnPoint {spawnPoint.gameObject.name} type isn't {spawnPointModule.SpawnPointType}");
                
                if(spawnPoint == null)
                    result.AddError($"SpawnPoint {spawnPoint.gameObject.name} not found");
            }
        }
    }
}
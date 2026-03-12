using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.CharacterSpawner.Extensions;
using Sources.EcsBoundedContexts.CharacterSpawner.Presentation.Types;
using UnityEngine;

namespace Sources.EcsBoundedContexts.CharacterSpawner.Presentation
{
    public class CharacterSpawnerModule : EntityModule, ISelfValidator
    {
        [Required] [field: SerializeField] public List<EntityLink> MeleeSpawnPoints { get; private set; }
        [Required] [field: SerializeField] public List<EntityLink> RangeSpawnPoints { get; private set; }
        
        public void Validate(SelfValidationResult result)
        {
            MeleeSpawnPoints.ValidateSpawnPoints<CharacterSpawnPointModule>(SpawnPointType.CharacterMelee, result);
            RangeSpawnPoints.ValidateSpawnPoints<CharacterSpawnPointModule>(SpawnPointType.CharacterRanged, result);
        }

        [Button]
        private void SetSpawnPoints()
        {
            MeleeSpawnPoints = gameObject.GetSpawnPoints<CharacterSpawnPointModule>(SpawnPointType.CharacterMelee);
            RangeSpawnPoints = gameObject.GetSpawnPoints<CharacterSpawnPointModule>(SpawnPointType.CharacterRanged);
        }
    }
}
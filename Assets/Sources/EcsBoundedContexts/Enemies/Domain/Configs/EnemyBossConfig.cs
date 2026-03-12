using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Domain.Configs
{    
    [CreateAssetMenu(fileName = nameof(EnemyBossConfig), menuName = "Configs/" + nameof(EnemyBossConfig), order = 51)]
    public class EnemyBossConfig : Config
    {
        [field: Range(1, 3)]
        [field: Required] [field: SerializeField]
        public float FindRange { get; private set; } = 3;        
        [field: Range(1, 3)]
        [field: Required] [field: SerializeField]
        public float MassAttackFindRange { get; private set; } = 3;        
        [field: Range(1, 3)]
        [field: Required] [field: SerializeField]
        public float MassAttackDelay { get; private set; } = 4;
        
        [Header("Movement")]
        [field: SerializeField] public float RotationSpeed { get; private set; } = 5f;
        [field: SerializeField] public float ChangeRotationSpeedDelta { get; private set; } = 5f;
    }
}
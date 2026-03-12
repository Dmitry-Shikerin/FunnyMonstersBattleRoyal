using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Enemies.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = "Configs/" + nameof(EnemyConfig), order = 51)]
    public class EnemyKamikazeConfig : Config
    {
        [field: Range(1, 3)]
        [field: SerializeField] public float FindRange { get; private set; } = 3;
        [field: SerializeField] public float MassAttackFindRange { get; private set; } = 4f;
        
        [Header("Movement")]
        [field: SerializeField] public float RotationSpeed { get; private set; } = 5f;
        [field: SerializeField] public float ChangeRotationSpeedDelta { get; private set; } = 5f;
    }
}
using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterRangeConfig), menuName = "Configs/" + nameof(CharacterRangeConfig), order = 51)]
    public class CharacterRangeConfig : Config
    {
        [Header("Stats")]
        [field: SerializeField] public float FindRange { get; private set; } = 10f;

        [field: SerializeField] public float RotationSpeed { get; private set; } = 5f;
        [field: SerializeField] public float ChangeRotationSpeedDelta { get; private set; } = 3f;
        [field: SerializeField] public int Health { get; private set; } = 50;
    }
}
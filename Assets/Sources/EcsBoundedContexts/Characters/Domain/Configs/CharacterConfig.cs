using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = "Configs/" + nameof(CharacterConfig), order = 51)]
    public class CharacterConfig : Config
    {
        [field: Header("Movement")]
        [field: SerializeField] public float Speed { get; private set; } = 10f;
        [field: SerializeField] public float GroundedGravity { get; private set; } = 3f;
        
        [field: Header("Jump")]
        [field: SerializeField] public float GroundedDistanceEndAnim { get; private set; } = 0.5f;
        [field: SerializeField] public float JumpDuration { get; private set; } = 2f;
        [field: SerializeField] public float JumpHeight { get; private set; } = 1.5f;
        
        [field: Header("Grounded")]
        [field: SerializeField] public float GroundRadius { get; private set; } = 0.3f;
        [field: SerializeField] public LayerMask GroundMask { get; private set; }    // Что считаем землей (например, Default, Grou
    }
}
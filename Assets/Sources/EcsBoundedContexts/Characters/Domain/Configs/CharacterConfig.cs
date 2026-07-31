using DG.Tweening;
using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(CharacterConfig), menuName = "Configs/" + nameof(CharacterConfig), order = 51)]
    public class CharacterConfig : Config
    {
        [field: Header("Movement")]
        [field: SerializeField] public float Speed { get; private set; } = 10f;
        [field: SerializeField] public float SpeedChangeDelta { get; private set; } = 10f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 2f;
        [field: SerializeField] public float MovementGravity { get; private set; } = -3f;

        [field: Header("Idle")]
        [field: SerializeField] public float IdleGravity { get; private set; } = 0.3f;

        [field: Header("Jump")]
        [field: SerializeField] public float GroundedDistanceEndAnim { get; private set; } = 0.5f;
        [field: SerializeField] public float ChangeJumpGravityDuration { get; private set; } = 2f;
        [field: SerializeField] public float JumpGravity { get; private set; } = 5f;
        [field: SerializeField] public float JumpForwardPower { get; private set; } = 50f;
        [field: SerializeField] public Ease ChangeJumpGravityEase { get; private set; }

        [field: Header("Fall")]
        [field: SerializeField] public float FallGravity { get; private set; }
        [field: SerializeField] public float ChangeFallGravityDuration { get; private set; }
        [field: SerializeField] public Ease ChangeFallGravityEase { get; private set; }

        [field: Header("Grounded")]
        [field: SerializeField] public float EndAirDistance { get; private set; } = 1f;
        [field: SerializeField] public float GroundRadius { get; private set; } = 0.3f;
        [field: SerializeField] public LayerMask GroundMask { get; private set; }
    }
}
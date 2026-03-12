using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.BurnAbilities.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(BurnConfig), menuName = "Configs/" + nameof(BurnConfig), order = 51)]
    public class BurnConfig : Config
    {
        [field: SerializeField] public float BurnTickDelay { get; private set; } = 1f;
        [field: SerializeField] public float ForbiddingUseDelay { get; private set; } = 2f;
        [field: SerializeField] public float BurnDuration { get; private set; } = 5f;
        [field: SerializeField] public int InstantDamage { get; private set; } = 7;
        [field: SerializeField] public int TickDamage { get; private set; } = 2;
    }
}
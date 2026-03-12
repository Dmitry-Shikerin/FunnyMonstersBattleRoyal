using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DailyRewards.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(DailyRewardConfig), menuName = "Configs/" + nameof(DailyRewardConfig), order = 51)]
    public class DailyRewardConfig : Config
    {
        [field: SerializeField] public int HealthBoostersAmount { get; private set; } = 5;
    }
}
using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(EnemySpawnStrategyCollector), menuName = "Configs/" + nameof(EnemySpawnStrategyCollector), order = 51)]
    public class EnemySpawnStrategyCollector : ConfigCollector<EnemySpawnStrategy>
    {
    }
}
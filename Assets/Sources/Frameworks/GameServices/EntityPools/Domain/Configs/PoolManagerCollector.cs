using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.Frameworks.GameServices.EntityPools.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(PoolManagerCollector), menuName = "Configs/" + nameof(PoolManagerCollector), order = 51)]
    public class PoolManagerCollector : ConfigCollector<PoolManagerConfig>
    {
    }
}
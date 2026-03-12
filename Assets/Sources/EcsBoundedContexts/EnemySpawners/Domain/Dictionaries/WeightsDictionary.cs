using System;
using Sources.EcsBoundedContexts.Enemies.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUtils.Dictionaries;

namespace Sources.EcsBoundedContexts.EnemySpawners.Domain.Dictionaries
{
    [Serializable]
    public class WeightsDictionary : SerializedDictionary<EnemyType, int>
    {
    }
}
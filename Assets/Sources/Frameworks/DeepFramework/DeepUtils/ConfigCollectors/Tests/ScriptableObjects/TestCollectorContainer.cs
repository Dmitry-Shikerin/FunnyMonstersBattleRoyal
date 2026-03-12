using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Tests.ScriptableObjects
{
    [CreateAssetMenu(fileName = "TestCollectorContainer", menuName = "Configs/TestCollectorContainer")]
    public class TestCollectorContainer : CollectorContainer<TestConfigCollector, TestConfig>
    {
    }
}
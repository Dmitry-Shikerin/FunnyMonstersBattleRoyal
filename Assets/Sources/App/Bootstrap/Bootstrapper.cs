using Sources.App.Core;
using Sources.App.Factories;
using Sources.Frameworks.DeepFramework.DeepCores.Domain.Constants;
using UnityEngine;

namespace Sources.App.Bootstrap
{
    [DefaultExecutionOrder(ExeOrder.Bootstrap)]
    public class Bootstrapper : MonoBehaviour
    {
        private AppCore _appCore;

        private void Awake()
        {
            _appCore = FindObjectOfType<AppCore>() ?? new AppCoreFactory().Create();
        }
    }
}

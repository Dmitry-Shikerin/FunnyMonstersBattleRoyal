using Fusion.Menu;
using Reflex.Core;
using Reflex.Enums;
using Sirenix.OdinInspector;
using Sources.BoundedContexts.NetworkCore.Services;
using UnityEngine;

namespace Sources.App.DIContainers.Common
{
    public class NetworkInstaller : MonoBehaviour, IInstaller
    {
        [Required] [SerializeField] private FusionMenuConfig _fusionMenuConfig;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            //Network
            containerBuilder.RegisterValue(_fusionMenuConfig);
            //containerBuilder.RegisterType(typeof(FusionMenuConnectArgs), Lifetime.Singleton, Reflex.Enums.Resolution.Lazy);
            containerBuilder.RegisterType(typeof(NetworkStartGameService), Lifetime.Singleton, Reflex.Enums.Resolution.Lazy);
        }
    }
}
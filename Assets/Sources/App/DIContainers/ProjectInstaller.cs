using System;
using Fusion;
using Reflex.Core;
using Reflex.Enums;
using Sources.BoundedContexts.Networks;
using Sources.Frameworks.GameServices.Curtains.Presentation.Implementation;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Domain;
using Sources.Frameworks.GameServices.SceneLoaderServices.Implementation;
using Sources.Frameworks.GameServices.Scenes.Services.Implementation;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using Sources.InfrastructureInterfaces.Services.SceneLoaderService;
using UnityEngine;
using Object = UnityEngine.Object;
using Resolution = Reflex.Enums.Resolution;

namespace Sources.App.DIContainers
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        private NetworkRunner _networkRunner;
        private NetworkSceneManagerDefault _networkSceneManagerDefault;
        private NetworkCallbacksReceiver _networkCallbacksReceiver;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType(typeof(PhotonSceneLoaderService), new []{typeof(ISceneLoaderService)}, Lifetime.Singleton, Resolution.Lazy);
            //containerBuilder.RegisterType(typeof(SceneLoaderService), new []{typeof(ISceneLoaderService)}, Lifetime.Singleton, Resolution.Lazy);
            //containerBuilder.RegisterType(typeof(AddressableSceneLoaderService), new []{typeof(ISceneLoaderService)}, Lifetime.Singleton, Resolution.Lazy);
            containerBuilder.RegisterType(typeof(SceneService), new []{typeof(ISceneService)}, Lifetime.Singleton, Resolution.Lazy);
            
            //Network
            _networkRunner = FindObjectOfType<NetworkRunner>();

            if (_networkRunner == null)
                throw new NullReferenceException("Runner not found");

            _networkSceneManagerDefault = _networkRunner.GetComponent<NetworkSceneManagerDefault>();
            _networkCallbacksReceiver = _networkRunner.GetComponent<NetworkCallbacksReceiver>();
            
            containerBuilder.RegisterValue(_networkRunner);
            containerBuilder.RegisterValue(_networkSceneManagerDefault);
            containerBuilder.RegisterValue(_networkCallbacksReceiver);

            //Curtain
             CurtainView curtainView =
                 Object.Instantiate(Resources.Load<CurtainView>(ResourcesPrefabPath.Curtain)) ??
                 throw new NullReferenceException(nameof(CurtainView));
             containerBuilder.RegisterValue(curtainView, new[] { typeof(ICurtainView) });
             curtainView.Hide();
        }
    }
}
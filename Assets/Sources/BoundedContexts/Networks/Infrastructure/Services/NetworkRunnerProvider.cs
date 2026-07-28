using Fusion;
using Sources.BoundedContexts.Networks.Core;
using Sources.EcsBoundedContexts.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.BoundedContexts.Networks.Infrastructure.Services
{
    public class NetworkRunnerProvider
    {
        private static NetworkRunner _runner;
        private static NetworkCallbacksReceiver _networkCallbacksReceiver;
        private static NetworkSceneManagerDefault _sceneManagerDefault;
        private static EcsGameStartUp _ecsGameStartUp;

        public static NetworkRunner Runner
        {
            get
            {
                if (_runner != null)
                    return _runner;

                _runner = CreateRunner();

                return _runner;
            }
        }

        public static NetworkSceneManagerDefault SceneManagerDefault
        {
            get
            {
                if (_sceneManagerDefault == null)
                    _runner = CreateRunner();

                return _sceneManagerDefault;
            }
        }

        public static NetworkCallbacksReceiver NetworkCallbacksReceiver
        {
            get
            {
                if (_networkCallbacksReceiver == null)
                    _runner = CreateRunner();

                return _networkCallbacksReceiver;
            }
        }

        public static EcsGameStartUp EcsGameStartUp
        {
            get
            {
                if (_ecsGameStartUp == null)
                    _runner = CreateRunner();

                return _ecsGameStartUp;
            }
        }

        private static NetworkRunner CreateRunner()
        {
            NetworkRunner networkRunner = new GameObject("NetworkCore").AddComponent<NetworkRunner>();
            networkRunner.AddComponent<NetworkRunnerMemento>();
            _sceneManagerDefault = networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            _networkCallbacksReceiver = networkRunner.gameObject.AddComponent<NetworkCallbacksReceiver>();
            _ecsGameStartUp = networkRunner.gameObject.AddComponent<EcsGameStartUp>();

            return networkRunner;
        }
    }
}
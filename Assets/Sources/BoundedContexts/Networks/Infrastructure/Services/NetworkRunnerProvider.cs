using Fusion;
using Sources.BoundedContexts.Networks.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.BoundedContexts.Networks.Infrastructure.Services
{
    public class NetworkRunnerProvider
    {
        private static NetworkRunner _runner;
        private static NetworkCallbacksReceiver _networkCallbacksReceiver;
        private static NetworkSceneManagerDefault _sceneManagerDefault;

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

        private static NetworkRunner CreateRunner()
        {
            NetworkRunner networkRunner = new GameObject("NetworkCore").AddComponent<NetworkRunner>();
            networkRunner.AddComponent<NetworkRunnerMemento>();
            _sceneManagerDefault = networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            _networkCallbacksReceiver = networkRunner.gameObject.AddComponent<NetworkCallbacksReceiver>();

            return networkRunner;
        }
    }
}
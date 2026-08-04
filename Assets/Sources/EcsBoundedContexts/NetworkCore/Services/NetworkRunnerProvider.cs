using Fusion;
using Sources.EcsBoundedContexts.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NetworkCore.Services
{
    public class NetworkRunnerProvider
    {
        private static NetworkRunner _runner;
        private static NetworkCallbacksReceiver _networkCallbacksReceiver;
        private static NetworkSceneManagerDefault _sceneManagerDefault;
        private static LeoEcsGameStartUp _leoEcsGameStartUp;

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

        public static LeoEcsGameStartUp LeoEcsGameStartUp
        {
            get
            {
                if (_leoEcsGameStartUp == null)
                    _runner = CreateRunner();

                return _leoEcsGameStartUp;
            }
        } 

        private static NetworkRunner CreateRunner()
        {
            NetworkRunner networkRunner = new GameObject("NetworkCore").AddComponent<NetworkRunner>();
            networkRunner.AddComponent<NetworkRunnerMemento>();
            _sceneManagerDefault = networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            _networkCallbacksReceiver = networkRunner.gameObject.AddComponent<NetworkCallbacksReceiver>();
            _leoEcsGameStartUp = networkRunner.gameObject.AddComponent<NetworkEcsRunner>().LeoEcsGameStartUp;

            return networkRunner;
        }
    }
}
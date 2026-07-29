using Fusion;
using Sources.EcsBoundedContexts.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.EcsBoundedContexts.NetworkCore
{
    public class NetworkRunnerProvider
    {
        private static NetworkRunner _runner;
        private static NetworkCallbacksReceiver _networkCallbacksReceiver;
        private static NetworkSceneManagerDefault _sceneManagerDefault;
        private static EcsGameStartUp _ecsGameStartUp;
        //private static JoinManager _joinManager;

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
        //
        // public static JoinManager JoinManager
        // {
        //     get
        //     {
        //         if (_joinManager == null)
        //             _runner = CreateRunner();
        //
        //         return _joinManager;
        //     }
        // }

        private static NetworkRunner CreateRunner()
        {
            NetworkRunner prefab = Resources.Load<NetworkRunner>("NetworkRunner");
            NetworkRunner networkRunner = Object.Instantiate(prefab);
            _sceneManagerDefault = networkRunner.gameObject.GetComponent<NetworkSceneManagerDefault>();
            _networkCallbacksReceiver = networkRunner.gameObject.GetComponent<NetworkCallbacksReceiver>();
            _ecsGameStartUp = networkRunner.gameObject.GetComponent<EcsGameStartUp>();
            //_joinManager = networkRunner.gameObject.GetComponent<JoinManager>();
            
            // NetworkRunner networkRunner = new GameObject("NetworkCore").AddComponent<NetworkRunner>();
            // networkRunner.AddComponent<NetworkRunnerMemento>();
            // _sceneManagerDefault = networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            // _networkCallbacksReceiver = networkRunner.gameObject.AddComponent<NetworkCallbacksReceiver>();
            // _ecsGameStartUp = networkRunner.gameObject.AddComponent<EcsGameStartUp>();
            // _joinManager = networkRunner.gameObject.AddComponent<JoinManager>();

            return networkRunner;
        }
    }
}
using Fusion;
using Sources.BoundedContexts.Networks.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace Sources.BoundedContexts.Networks.Infrastructure.Services
{
    public class NetworkRunnerProvider
    {
        private static NetworkRunner _runner;

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
        
        public static NetworkSceneManagerDefault SceneManagerDefault { get; private set; }
        public static NetworkCallbacksReceiver NetworkCallbacksReceiver { get; private set; }
        
        private static NetworkRunner CreateRunner()
        {
            NetworkRunner networkRunner = new GameObject("NetworkCore").AddComponent<NetworkRunner>();
            networkRunner.AddComponent<NetworkRunnerMemento>();
            SceneManagerDefault = networkRunner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            NetworkCallbacksReceiver = networkRunner.gameObject.AddComponent<NetworkCallbacksReceiver>();
            networkRunner.gameObject.AddComponent<JoinManager>();

            return networkRunner;
        }
    }
}
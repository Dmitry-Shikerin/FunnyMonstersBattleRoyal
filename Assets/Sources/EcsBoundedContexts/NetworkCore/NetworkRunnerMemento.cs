using UnityEngine;

namespace Sources.EcsBoundedContexts.NetworkCore
{
    public class NetworkRunnerMemento : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
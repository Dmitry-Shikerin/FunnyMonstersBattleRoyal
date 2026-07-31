using UnityEngine;

namespace Sources.EcsBoundedContexts.NetworkCore.Services
{
    public class NetworkRunnerMemento : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
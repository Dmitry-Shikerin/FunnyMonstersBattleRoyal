using UnityEngine;

namespace Sources.BoundedContexts.NetworkCore.Services
{
    public class NetworkRunnerMemento : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
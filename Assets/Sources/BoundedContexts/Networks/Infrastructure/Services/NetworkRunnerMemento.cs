using System;
using UnityEngine;

namespace Sources.BoundedContexts.Networks.Infrastructure.Services
{
    public class NetworkRunnerMemento : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
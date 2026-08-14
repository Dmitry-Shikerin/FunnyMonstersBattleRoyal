using Fusion;
using UnityEngine;

namespace Sources.BoundedContexts.Spawners.Presentation
{
    public class SpawnPointView : NetworkBehaviour
    {
        [field: SerializeField] public Transform SpawnPositionTransform { get; private set; }
        
        [Networked]
        public NetworkBool IsBusy { get; set; }

        public void SetBusy(bool isBusy)
        {
            if(Runner.IsClient)
                return;

            IsBusy = isBusy;
        }
    }
}
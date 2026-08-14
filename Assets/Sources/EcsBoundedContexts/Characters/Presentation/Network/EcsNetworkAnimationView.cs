using Fusion;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Presentation.Network
{
    public class EcsNetworkAnimationView : NetworkBehaviour
    {
        [Required] [SerializeField] private EntityLink _character;
        
        [Rpc(RpcSources.StateAuthority, RpcTargets.All, InvokeLocal = false)]
        public void PlayAnimation_Rpc(int animationName)
        {
            if (_character.Entity == default)
                return;
            
            _character.Entity.PlayAnimation((AnimationName)animationName);
        }
    }
}
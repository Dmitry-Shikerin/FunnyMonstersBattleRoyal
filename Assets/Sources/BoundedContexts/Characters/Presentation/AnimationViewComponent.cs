using System;
using Animancer;
using Fusion;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Animancers.Domain.Configs;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Presentation
{
    public class AnimationViewComponent : NetworkBehaviour, IViewComponent
    {
        [SerializeField] private AnimancerComponent _animancer;

        private AnimationConfig _config;

        public PlayerRef PlayerRef { get; private set; }

        [Inject]
        private void Construct(IAssetCollector assetCollector)
        {
            _config = assetCollector.Get<AnimationConfig>();
        }

        public void Init(PlayerRef playerRef)
        {
            PlayerRef = playerRef;
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All, InvokeLocal = false)]
        public void Play_Rpc(int animationName)
        {
            Play((AnimationName)animationName);
        }

        public AnimancerState Play(AnimationName animationName) =>
            _animancer.Play(_config.Animations[animationName]);

        public AnimancerState SetCallback(AnimancerState state, AnimationEventName eventName, Action callback)
        {
            StringReference reference = _config.AnimationNames[eventName];
            state.Events(state).SetCallback(reference.String, callback);
            return state;
        }
    }
}
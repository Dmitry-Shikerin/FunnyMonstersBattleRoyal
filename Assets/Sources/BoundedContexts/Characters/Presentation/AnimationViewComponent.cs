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

        [Networked]
        private int AnimationIndex { get; set; }
        [Networked]
        [OnChangedRender(nameof(ApplyAnimSpeed))]
        public float AnimationSpeed { get; set; }
        
        private AnimationConfig _config;
        private LinearMixerState _walkState;
        private bool _isInitialized;

        public PlayerRef PlayerRef { get; private set; }

        [Inject]
        private void Construct(IAssetCollector assetCollector) =>
            _config = assetCollector.Get<AnimationConfig>();

        public void Init(PlayerRef playerRef)
        {
            PlayerRef = playerRef;
            _isInitialized = true;

            if (Runner.IsClient == false)
                return;

            PlayAnimation((AnimationName)AnimationIndex);
        }

        public void PlayAnim(AnimationName animationName)
        {
            if (Runner.IsClient)
            {
                Play(animationName);
                return;
            }

            AnimationIndex = (int)animationName;
            Play(animationName);
            Play_Rpc(AnimationIndex);
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All, InvokeLocal = false)]
        private void Play_Rpc(int animationName)
        {
            Play((AnimationName)animationName);
        }

        private void Play(AnimationName animationName)
        {
            if (animationName == AnimationName.Walk)
            {
                PlayWalk();
                return;
            }

            PlayAnimation(animationName);
        }

        private void PlayWalk()
        {
            if (_isInitialized == false)
                return;
            
            AnimancerState state = PlayAnimation(AnimationName.Walk);

            if (state is not LinearMixerState linearMixerState)
                throw new InvalidOperationException();

            _walkState = linearMixerState;
        }

        private AnimancerState PlayAnimation(AnimationName animationName)
        {
            if (_isInitialized == false)
                return null;
            
            return _animancer.Play(_config.Animations[animationName]);
        }

        private AnimancerState SetCallback(AnimancerState state, AnimationEventName eventName, Action callback)
        {
            StringReference reference = _config.AnimationNames[eventName];
            state.Events(state).SetCallback(reference.String, callback);
            return state;
        }

        private void ApplyAnimSpeed()
        {
            if (_isInitialized == false)
                return;

            if (_walkState == null)
                return;

            if ((AnimationName)AnimationIndex != AnimationName.Walk)
                return;

            _walkState.Parameter = AnimationSpeed;
        }
    }
}
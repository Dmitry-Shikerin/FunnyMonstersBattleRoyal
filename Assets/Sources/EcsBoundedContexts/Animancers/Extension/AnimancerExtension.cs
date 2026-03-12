using System;
using Animancer;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Animancers.Domain.Configs;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.EcsBoundedContexts.Animancers.Extension
{
    public static class AnimancerExtension
    {
        private static AnimationConfig s_config;
        
        public static void Construct(IAssetCollector assetCollector)
        {
            s_config = assetCollector.Get<AnimationConfig>();
        }
        
        public static AnimancerState PlayAnimation(this ProtoEntity entity, AnimationName animationName)
        {
            var transition = s_config.Animations[animationName];
            return entity.GetAnimancerEcs().Value.Play(transition);
        }

        public static AnimancerState SetCallback(this AnimancerState state, AnimationEventName eventName, Action callback)
        {
            StringReference reference = s_config.AnimationNames[eventName];
            state.Events(state).SetCallback(reference.String, callback);
            return state;
        }   
        
        public static bool IsPlayingAnimation(this ProtoEntity entity, AnimationName animationName)
        {
            var reference = s_config.Animations[animationName].Transition;
            return entity.GetAnimancerEcs().Value.IsPlaying(reference);
        }
    }
}
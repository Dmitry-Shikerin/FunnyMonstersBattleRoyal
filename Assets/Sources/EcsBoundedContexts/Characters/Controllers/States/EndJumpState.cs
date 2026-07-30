using Animancer;
using DG.Tweening;
using Leopotam.EcsProto;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Animancers.Domain.Configs;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Characters.Presentation.Network;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.EcsBoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class EndJumpState : FSMState
    {
        private ProtoEntity _entity;
        
        private AnimancerLayer _runLayer;
        private AnimancerLayer _landLayer;
        private AnimationConfig _animConfig;
        private AnimancerComponent _animancer;

        [Inject]
        private void Construct(IAssetCollector collector)
        {
            _animConfig = collector.Get<AnimationConfig>();
        }
        
        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;
        
        protected override void OnEnter()
        {
            // _animancer = _entity.GetAnimancerEcs().Value;
            // _runLayer = _animancer.Layers[0];
            // _landLayer = _animancer.Layers[1];
            // _animancer.Layers.SetMask(1, _animConfig.AvatarMask);
            // _landLayer.SetDebugName("Landing Layer");
            // _landLayer
            //     .Play(_animConfig.Animations[AnimationName.EndJump])
            //     .SetCallback(AnimationEventName.End, Finish)
            //     .SetCallback(AnimationEventName.PlayRun, () =>_runLayer.Play(_animConfig.Animations[AnimationName.Walk]));
            DOVirtual.DelayedCall(0.25f, Finish);
            _entity.PlayAnimation(AnimationName.EndJump);
            NetworkAnimationView networkAnimationView = _entity.GetCharacterModule().Value.NetworkAnimationView;
            networkAnimationView.PlayAnimation_Rpc((int)AnimationName.EndJump);
        }

        protected override void OnExit()
        {
            _entity.DelAir();
            _entity.DelJumping();
            // _runLayer.DestroyStates();
            // _landLayer.DestroyStates();
        }
    }
}
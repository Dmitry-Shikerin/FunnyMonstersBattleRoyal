using Leopotam.EcsProto;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Characters.Presentation.Network;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.DeepFramework.DeepUtils.SignalBuses.StreamBuses.Interfaces;

namespace Sources.EcsBoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterStartJumpState : FSMState
    {
        private ProtoEntity _entity;
        private ISignalBus _signalBus;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        [Inject]
        private void Construct(ISignalBus signalBus) =>
            _signalBus = signalBus;
        
        protected override void OnEnter()
        {
            _entity.PlayAnimation(AnimationName.StartJump).SetCallback(AnimationEventName.End, Finish);
            NetworkAnimationView networkAnimationView = _entity.GetCharacterModule().Value.NetworkAnimationView;
            networkAnimationView.PlayAnimation_Rpc((int)AnimationName.StartJump);
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            _entity.AddJumping(config.JumpDuration, 0, _entity.GetTransform().Value.position);
        }

        protected override void OnUpdate()
        {
        }
    }
}
using Leopotam.EcsProto;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Animancers.Domain.Enums;
using Sources.EcsBoundedContexts.Animancers.Extension;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;

namespace Sources.EcsBoundedContexts.Characters.Controllers.States
{
    [Category(NcCategoriesConst.Characters)]
    public class EndJumpState : FSMState
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity)
        {
            _entity = entity;
        }
        
        protected override void OnEnter()
        {
            _entity.PlayAnimation(AnimationName.EndJump).SetCallback(AnimationEventName.End, Finish);
        }

        protected override void OnExit()
        {
            _entity.DelAir();
            _entity.DelJumping();
        }
    }
}
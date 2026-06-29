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
    public class CharacterAirJumpState : FSMState
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity)
        {
            _entity = entity;
        }
        
        protected override void OnEnter()
        {
            _entity.PlayAnimation(AnimationName.AirJump).SetCallback(AnimationEventName.End, Finish);
            _entity.AddAir();
        }
    }
}
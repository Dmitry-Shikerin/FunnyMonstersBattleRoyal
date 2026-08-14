using Fusion;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class InputInterpolatorAction : ActionTask
    {
        private CharacterMovementViewComponent _characterMovement;
        private InputReceiverViewComponent _inputReceiver;
        private NetworkRunner _runner;

        protected override string OnInit()
        {
            ViewComponentsLink link = blackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _characterMovement = link.Get<CharacterMovementViewComponent>();
            _inputReceiver = link.Get<InputReceiverViewComponent>();
            _runner = _characterMovement.Runner;
            return null;
        }

        protected override void OnUpdate()
        {
            Vector3 inputDirection = _inputReceiver.MovementDirection;
            
            if (inputDirection.Equals(Vector3.zero) && _characterMovement.CharacterDirection.Equals(Vector3.zero) == false)
            {
                _characterMovement.CharacterDirection = Vector3.MoveTowards(_characterMovement.CharacterDirection, Vector3.zero, 1f * _runner.DeltaTime);
            }
            else
            {
                _characterMovement.CharacterDirection = inputDirection;
            }
        }
    }
}
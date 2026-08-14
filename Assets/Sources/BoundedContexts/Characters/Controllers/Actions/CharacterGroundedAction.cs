using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterGroundedAction : ActionTask
    {
        private ViewComponentsLink _link;
        private CharacterConfig _config;
        private CharacterMovementViewComponent _characterMovement;
        private Transform _groundCheckTransform;

        [Inject]
        private void Construct(IAssetCollector assetCollector) =>
            _config = assetCollector.Get<CharacterConfig>();

        protected override string OnInit()
        {
            _link = blackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _characterMovement = _link.Get<CharacterMovementViewComponent>();
            _groundCheckTransform = _characterMovement.GroundCheckTransform;
            return null;
        }

        protected override void OnUpdate()
        {
            //bool isGrounded = IsControllerGrounded();
            bool isGrounded = IsCustomGrounded();

            if (isGrounded && _characterMovement.IsGrounded == false)
                _characterMovement.IsGrounded = true;
            else if (isGrounded == false && _characterMovement.IsGrounded)
                _characterMovement.IsGrounded = false;
        }

        private bool IsCustomGrounded() =>
            Physics.CheckSphere(_groundCheckTransform.position, _config.GroundRadius, _config.GroundMask);

        private bool IsControllerGrounded() =>
            _characterMovement.CharacterController.isGrounded;
    }
}
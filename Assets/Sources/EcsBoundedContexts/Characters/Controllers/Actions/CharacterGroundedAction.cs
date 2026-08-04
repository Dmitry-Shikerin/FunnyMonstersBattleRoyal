using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class CharacterGroundedAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnUpdate()
        {
            //bool isGrounded = IsControllerGrounded();
            bool isGrounded = IsCustomGrounded();

            if (isGrounded && _entity.HasGrounded() == false)
                _entity.AddGrounded();
            else if (isGrounded == false && _entity.HasGrounded())
                _entity.DelGrounded();
        }

        private bool IsCustomGrounded()
        {
            Transform groundCheck = _entity.GetCharacterModule().Value.GroundCheck;
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            return Physics.CheckSphere(groundCheck.position, config.GroundRadius, config.GroundMask);
        }

        private bool IsControllerGrounded() =>
            _entity.GetCharacterController().Value.isGrounded;
    }
}
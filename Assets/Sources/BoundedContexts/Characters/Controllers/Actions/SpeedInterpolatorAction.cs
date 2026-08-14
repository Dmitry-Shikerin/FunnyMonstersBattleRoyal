using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.BoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class SpeedInterpolatorAction : ActionTask
    {
        private ProtoEntity _entity;
        private CharacterConfig _config;
        private CharacterMovementViewComponent _movement;
        private InputReceiverViewComponent _input;

        [Inject]
        private void Construct(IAssetCollector assetCollector)
        {
            _config = assetCollector.Get<CharacterConfig>();
        }
        
        protected override string OnInit()
        {
            ViewComponentsLink link = blackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _movement = link.Get<CharacterMovementViewComponent>();
            _input = link.Get<InputReceiverViewComponent>();
            return null;
        }

        protected override void OnUpdate()
        {
            float speed = _movement.CharacterSpeed;
            Vector3 input = _input.MovementDirection;

            if (input == Vector3.zero)
            {
                if (speed > 0)
                {
                    _movement.CharacterSpeed -= _config.SpeedChangeDelta;
                }
            }
            else
            {
                if (speed < _config.Speed)
                {
                    _movement.CharacterSpeed += _config.SpeedChangeDelta;
                }
            }
        }
    }
}
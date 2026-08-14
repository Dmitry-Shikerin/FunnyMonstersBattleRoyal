using Fusion;
using Leopotam.EcsProto;
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
    public class RotationAction : ActionTask
    {
        private ProtoEntity _entity;
        private CharacterConfig _config;
        private CharacterMovementViewComponent _characterMovement;
        private ViewComponentsLink _link;
        private NetworkRunner _runner;

        [Inject]
        private void Construct(IAssetCollector assetCollector)
        {
            _config = assetCollector.Get<CharacterConfig>();
        }
        
        protected override string OnInit()
        {
            _link = blackboard.GetVariable<ViewComponentsLink>("_viewComponentsLink").value;
            _characterMovement = _link.Get<CharacterMovementViewComponent>();
            _runner = _characterMovement.Runner;
            return null;
        }

        protected override void OnUpdate()
        {
            Vector3 direction = _characterMovement.CharacterDirection;

            if (direction == Vector3.zero)
                return;
            
            Transform transform = _link.transform;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _config.RotationSpeed * _runner.DeltaTime);

        }
    }
}
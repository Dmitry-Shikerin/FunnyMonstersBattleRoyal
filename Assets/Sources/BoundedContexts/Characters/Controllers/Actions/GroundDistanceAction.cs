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
    public class GroundDistanceAction : ActionTask
    {
        private ProtoEntity _entity;
        private ViewComponentsLink _link;
        private CharacterMovementViewComponent _characterMovement;
        private Transform _groundCheckTransform;
        private CharacterConfig _config;

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
            LayerMask mask = _config.GroundMask;
            Ray ray = new Ray(_groundCheckTransform.position, Vector3.down);
            
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mask) == false)
                return;

            _characterMovement.GroundDistance = hit.distance;
        }
    }
}
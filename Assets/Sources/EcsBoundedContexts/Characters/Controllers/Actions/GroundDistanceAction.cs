using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class GroundDistanceAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnUpdate()
        {
            Transform groundedCheck = _entity.GetCharacterModule().Value.GroundCheck;
            LayerMask mask = _entity.GetCharacterConfig().Value.GroundMask;
                
            Ray ray = new Ray(groundedCheck.position, Vector3.down);
                
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mask) == false)
                return;
                
            _entity.ReplaceGroundDistance(hit.distance);
        }
    }
}
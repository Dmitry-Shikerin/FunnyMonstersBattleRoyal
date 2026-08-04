using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Input.Domain;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class InputInterpolatorAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnUpdate()
        {
            Vector3 inputDirection = _entity.GetInputEntity().Value.GetDirection().Value;
            ref DirectionComponent characterDirection = ref _entity.GetDirection();
            
            if (inputDirection.Equals(Vector3.zero) && characterDirection.Value.Equals(Vector3.zero) == false)
            {
                characterDirection.Value = Vector3.MoveTowards(characterDirection.Value, Vector3.zero, 1f * Time.deltaTime);
            }
            else
            {
                characterDirection.Value = inputDirection;
            }
        }
    }
}
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
    public class ChangeGravityAction : ActionTask
    {
        private ProtoEntity _entity;

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnUpdate()
        {
            CharacterConfig config = _entity.GetCharacterConfig().Value;
            float gravity = _entity.GetGravity().Value;
            float targetGravity = _entity.GetTargetGravity().Value;

            float currentGravity = Mathf.Lerp(gravity, targetGravity, config.ChangeGravityDelta);

            _entity.ReplaceGravity(currentGravity);
        }
    }
}
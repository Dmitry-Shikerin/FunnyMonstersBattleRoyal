using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class RotationAction : ActionTask
    {
        private ProtoEntity _entity;
        private CharacterConfig _config;

        [Inject]
        private void Construct(IAssetCollector assetCollector)
        {
            _config = assetCollector.Get<CharacterConfig>();
        }

        [Construct]
        private void Construct(ProtoEntity entity) =>
            _entity = entity;

        protected override void OnUpdate()
        {
            Vector3 direction = _entity.GetDirection().Value;

            if (direction == Vector3.zero)
                return;
            
            Transform transform = _entity.GetTransform().Value;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _config.RotationSpeed * Time.deltaTime);

        }
    }
}
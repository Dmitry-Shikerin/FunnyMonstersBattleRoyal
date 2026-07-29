using Leopotam.EcsProto;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Reflex.Attributes;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Characters.Domain.Configs;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUtils.Reflections.Attributes;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Actions
{
    [Category(NcCategoriesConst.Characters)]
    public class SpeedInterpolatorAction : ActionTask
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
            ref SpeedComponent speed = ref _entity.GetSpeed();
            Vector3 input = _entity.GetInputEntity().Value.GetDirection().Value;

            if (input == Vector3.zero)
            {
                if (speed.Value > 0)
                {
                    speed.Value -= _config.SpeedChangeDelta;
                }
            }
            else
            {
                if (speed.Value < _config.Speed)
                {
                    speed.Value += _config.SpeedChangeDelta;
                }
            }
        }
    }
}
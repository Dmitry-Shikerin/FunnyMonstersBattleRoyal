using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Lights.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Infrastructure
{
    public class LightEntityFactory : EntityFactory
    {
        public LightEntityFactory(
            IEntityRepository repository, 
            ProtoWorld world, 
            GameAspect aspect, 
            DiContainer container) 
            : base(
                repository,
                world,
                aspect,
                container)
        {
        }

        public override ProtoEntity Create(EntityLink link)
        {
            ShadowControllerModule module = link.GetModule<ShadowControllerModule>();
            Aspect.Light.NewEntity(out ProtoEntity entity);
            Authoring(link, entity);
            
            var light = module.m_Light;
            entity.ReplaceLight(
                module.m_Light, 
                light.range, 
                light.intensity, 
                light.color, 
                light.shadows != LightShadows.None,
                light.shadowStrength, light.shadows, light.shadowResolution);
            entity.AddShadowController(
                module.m_Importance,
                module.m_DistanceMode,
                module.m_LightRangeShadowDistanceCoeff,
                module.m_CustomMaxShadowDistance,
                module.m_IntensityReductionMode,
                module.m_CustomIntensityReductionCoeff,
                module.m_RangeReductionMode,
                module.m_CustomRangeReductionCoeff,
                module.m_IsShadowEnabled,
                module.m_IsResolutionReduced);
            
            return entity;
        }
    }
}
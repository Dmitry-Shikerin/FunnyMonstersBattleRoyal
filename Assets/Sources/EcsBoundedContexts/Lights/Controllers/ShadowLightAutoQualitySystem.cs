using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Lights.Domain.Configs;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Lights.Controllers
{
    public class ShadowLightAutoQualitySystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IAssetCollector _assetCollector;

        private ShadowManagerConfigCollector _config;

        public ShadowLightAutoQualitySystem(IAssetCollector assetCollector)
        {
            _assetCollector = assetCollector;
        }

        public void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<ShadowManagerConfigCollector>();
        }

        public void Run()
        {
            int level = QualitySettings.GetQualityLevel();
            _config.SetCurrentByIndex(level);
        }
    }
}
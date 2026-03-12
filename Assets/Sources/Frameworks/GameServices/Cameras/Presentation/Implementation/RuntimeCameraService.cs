using System;
using MyDependencies.Sources.Attributes;
using Sirenix.OdinInspector;
using Sources.EcsBoundedContexts.Cameras.Infrastructure.Services;
using Sources.Frameworks.DeepFramework.DeepUtils.Singletones;

namespace Sources.Frameworks.GameServices.Cameras.Presentation.Implementation
{
    public class RuntimeCameraService : MonoBehaviourSingleton<RuntimeCameraService>
    {
        private ICameraService _cameraService;

        [Button]
        public void PlayBattle()
        {
            //_cameraService.PlayDirector(DirectorId.Battle);
        }

        [Inject]
        private void Construct(ICameraService cameraService) =>
            _cameraService = cameraService ?? throw new ArgumentNullException(nameof(cameraService));
    }
}
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Cameras.Infrastructure;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Cameras.Controllers
{
    [EcsSystem(51)]
    [ComponentGroup(ComponentGroup.Camera)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public class MainCameraInitializeSystem : IProtoInitSystem
    {
        private readonly MainCameraEntityFactory _factory;

        public MainCameraInitializeSystem(
            MainCameraEntityFactory factory)
        {
            _factory = factory;
        }

        public void Init(IProtoSystems systems)
        {
            //_factory.Create(_root.MainCamera);
        }
    }
}
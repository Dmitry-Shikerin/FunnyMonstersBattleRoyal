using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Cameras.Infrastructure;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Cameras.Controllers
{
    [EcsSystem(51)]
    [ComponentGroup(ComponentGroup.Camera)]
    [Aspect(AspectName.Game)]
    public class MainCameraInitializeSystem : IProtoInitSystem
    {
        private readonly RootGameObject _rootGameObject;
        private readonly MainCameraEntityFactory _factory;

        public MainCameraInitializeSystem(
            RootGameObject rootGameObject,
            MainCameraEntityFactory factory)
        {
            _rootGameObject = rootGameObject;
            _factory = factory;
        }

        public void Init(IProtoSystems systems)
        {
            _factory.Create(_rootGameObject.MainCamera);
        }
    }
}
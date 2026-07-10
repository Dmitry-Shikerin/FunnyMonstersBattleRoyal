using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Sources.EcsBoundedContexts.Cameras.Infrastructure;
using Sources.EcsBoundedContexts.Cameras.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Cameras.Controllers
{
    [EcsSystem(51)]
    [ComponentGroup(ComponentGroup.Camera)]
    [Aspect(AspectName.Game)]
    public class MainCameraInitializeSystem : IProtoInitSystem
    {
        private readonly IEntityRepository _repository;
        private readonly RootGameObject _rootGameObject;
        private readonly MainCameraEntityFactory _factory;

        public MainCameraInitializeSystem(
            IEntityRepository repository,
            RootGameObject rootGameObject,
            MainCameraEntityFactory factory)
        {
            _repository = repository;
            _rootGameObject = rootGameObject;
            _factory = factory;
        }

        public void Init(IProtoSystems systems)
        {
            _rootGameObject.MainCamera.GetModule<MainCameraModule>().Cameras[VirtualCameraType.ThirdPerson].Follow =
                _repository.GetByName(IdsConst.Player).GetTransform().Value; 
            _factory.Create(_rootGameObject.MainCamera);
        }
    }
}
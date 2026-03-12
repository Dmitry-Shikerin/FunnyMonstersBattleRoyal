using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using MyDependencies.Sources.Containers;
using Sources.EcsBoundedContexts.Cameras.Infrastructure.Services;
using Sources.EcsBoundedContexts.Cameras.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Cameras.Infrastructure
{
    //TODO добавить фабрики в фактори коллектор и сделать генерацию для фабрик
    public class MainCameraEntityFactory : EntityFactory
    {
        private readonly ICameraService _cameraService;
        private readonly IEntityRepository _repository;

        public MainCameraEntityFactory(
            ICameraService cameraService,
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
            _cameraService = cameraService;
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            MainCameraModule module = link.GetModule<MainCameraModule>();
            
            Aspect.MainCamera.NewEntity(out ProtoEntity entity);
            //_repository.AddByName(entity, EntityIdsConst.MainCamera);
            Authoring(link, entity);
            
            entity.AddCamera(module.Camera);
            entity.AddTransform(link.transform);
            //Transform playerHeadTransform = _repository.GetByName(EntityIdsConst.Player).GetHeadTransform().Value;
            //_cameraService.Initialize(module.Cameras, VirtualCameraType.Rotate, playerHeadTransform);
            //Transform playerTransform = _repository.GetByName(EntityIdsConst.Player).GetTransform().Value;
            //_cameraService.SetFollower(VirtualCameraType.Rotate, playerTransform);
            
            return entity;
        }
    }
}
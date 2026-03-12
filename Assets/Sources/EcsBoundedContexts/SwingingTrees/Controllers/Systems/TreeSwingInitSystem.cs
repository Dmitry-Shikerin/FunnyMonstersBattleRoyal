using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SwingingTrees.Infrastructure.Factories;

namespace Sources.EcsBoundedContexts.SwingingTrees.Controllers.Systems
{
    [EcsSystem(16)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class TreeSwingInitSystem : IProtoInitSystem
    {
        private readonly TreeSwingEntityFactory _entityFactory;
        private readonly RootGameObject _rootGameObject;

        public TreeSwingInitSystem(
            TreeSwingEntityFactory entityFactory,
            RootGameObject rootGameObject)
        {
            _entityFactory = entityFactory;
            _rootGameObject = rootGameObject;
        }

        public void Init(IProtoSystems systems)
        {
            foreach (EntityLink link in _rootGameObject.Gras)
                _entityFactory.Create(link);
        }
    }
}
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Spawners.Infrastructure;

namespace Sources.EcsBoundedContexts.Spawners.Controllers
{
    [EcsSystem(11)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class SpawnPointsInitializeSystem : IProtoInitSystem
    {
        private readonly RootGameObject _rootGameObject;
        private readonly SpawnPointEntityFactory _spawnPointEntityFactory;

        public SpawnPointsInitializeSystem(
            RootGameObject rootGameObject,
            SpawnPointEntityFactory spawnPointEntityFactory)
        {
            _rootGameObject = rootGameObject;
            _spawnPointEntityFactory = spawnPointEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            foreach (EntityLink link in _rootGameObject.CharacterSpawnPoints)
                _spawnPointEntityFactory.Create(link);
        }
    }
}
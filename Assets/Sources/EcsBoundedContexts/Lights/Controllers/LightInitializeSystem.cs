using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Lights.Infrastructure;

namespace Sources.EcsBoundedContexts.Lights.Controllers
{
    public class LightInitializeSystem : IProtoInitSystem
    {
        private readonly RootGameObject _root;
        private readonly LightEntityFactory _entityFactory;

        public LightInitializeSystem(
            RootGameObject root,
            LightEntityFactory entityFactory)
        {
            _root = root;
            _entityFactory = entityFactory;
        }

        public void Init(IProtoSystems systems)
        {
        }
    }
}
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(10)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class CharacterInitializeSystem : IProtoInitSystem
    {
        private readonly CharacterEntityFactory _entityFactory;
        private readonly RootGameObject _rootGameObject;

        public CharacterInitializeSystem(
            CharacterEntityFactory entityFactory,
            RootGameObject rootGameObject)
        {
            _entityFactory = entityFactory;
            _rootGameObject = rootGameObject;
        }

        public void Init(IProtoSystems systems)
        {
            EntityLink characterLink = _rootGameObject.Character;
            _entityFactory.Create(characterLink);
        }
    }
}
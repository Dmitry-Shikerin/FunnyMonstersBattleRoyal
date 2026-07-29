using Leopotam.EcsProto;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Characters.Infrastructure;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Input.Infrastructure;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(10)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class CharacterInitializeSystem : IProtoInitSystem
    {
        private readonly IAssetCollector _assetCollector;
        private readonly CharacterEntityFactory _entityFactory;
        private readonly RootGameObject _rootGameObject;

        public CharacterInitializeSystem(
            InputEntityFactory factory,
            IAssetCollector assetCollector,
            CharacterEntityFactory entityFactory,
            RootGameObject rootGameObject)
        {
            _assetCollector = assetCollector;
            _entityFactory = entityFactory;
            _rootGameObject = rootGameObject;
        }

        public void Init(IProtoSystems systems)
        {
             // EntityLink characterLink = _rootGameObject.Character;
             // CharacterModule module = _assetCollector.Get<CharacterModule>();
             // Vector3 position = _rootGameObject.CharacterSpawnPoint.position;
             // Quaternion rotation = _rootGameObject.CharacterSpawnPoint.rotation;
             //
             // // if (PhotonNetwork.IsConnected)
             // //     module = PhotonNetwork.Instantiate("Character", position, rotation).GetComponent<CharacterModule>();
             // // else
             // module = Object.Instantiate(module, position, rotation);
             // Debug.Log($"Init character");
             //
             // _entityFactory.Create(module.GetComponent<EntityLink>());
        }
    }
}
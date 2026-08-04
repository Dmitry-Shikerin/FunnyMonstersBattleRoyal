using Fusion;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Sources.EcsBoundedContexts.Cameras.Infrastructure;
using Sources.EcsBoundedContexts.Cameras.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Input.Infrastructure;
using Sources.EcsBoundedContexts.Spawners.Infrastructure.Services;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Infrastructure
{
    public class CharacterFactory
    {
        private readonly SpawnPointEntitiesProvider _spawnPointEntitiesProvider;
        private readonly MainCameraEntityFactory _mainCameraEntityFactory;
        private readonly RootGameObject _rootGameObject;
        private readonly CharacterEntityFactory _characterEntityFactory;
        private readonly InputEntityFactory _inputEntityFactory;

        public CharacterFactory(
            SpawnPointEntitiesProvider spawnPointEntitiesProvider,
            MainCameraEntityFactory mainCameraEntityFactory,
            RootGameObject rootGameObject,
            CharacterEntityFactory characterEntityFactory,
            InputEntityFactory inputEntityFactory)
        {
            _spawnPointEntitiesProvider = spawnPointEntitiesProvider;
            _mainCameraEntityFactory = mainCameraEntityFactory;
            _rootGameObject = rootGameObject;
            _characterEntityFactory = characterEntityFactory;
            _inputEntityFactory = inputEntityFactory;
        }

        public NetworkObject Create(NetworkPrefabRef prefab, PlayerRef playerRef, NetworkRunner runner)
        {
            ProtoEntity spawnPointEntity = _spawnPointEntitiesProvider.GetFreedomPoint();
            Vector3 position = spawnPointEntity.GetSpawnPointTransform().Value.position;
            Quaternion rotation = spawnPointEntity.GetSpawnPointTransform().Value.rotation;
            position.y += 10;

            NetworkObject networkObject = runner.Spawn(prefab, position, rotation, playerRef);
            spawnPointEntity.AddBusy();
            
            ProtoEntity inputEntity = _inputEntityFactory.Create(null);
            ProtoEntity characterEntity = _characterEntityFactory.Create(networkObject.GetComponent<EntityLink>());
            characterEntity.AddPlayerRef(playerRef);
            characterEntity.AddInputEntity(inputEntity);
            inputEntity.AddInputOwner(characterEntity);

            if (runner.LocalPlayer == playerRef)
            {
                _rootGameObject.MainCamera.GetModule<MainCameraModule>().Cameras[VirtualCameraType.ThirdPerson].Follow =
                    characterEntity.GetTransform().Value;
                _mainCameraEntityFactory.Create(_rootGameObject.MainCamera);
            }
            
            return networkObject;
        }

        public ProtoEntity Create(NetworkObject networkObject, PlayerRef playerRef, NetworkRunner runner)
        {
            ProtoEntity inputEntity = _inputEntityFactory.Create(null);
            ProtoEntity characterEntity = _characterEntityFactory.Create(networkObject.GetComponent<EntityLink>());
            characterEntity.AddPlayerRef(playerRef);
            characterEntity.AddInputEntity(inputEntity);
            inputEntity.AddInputOwner(characterEntity);

            if (runner.LocalPlayer == playerRef)
            {
                _rootGameObject.MainCamera.GetModule<MainCameraModule>().Cameras[VirtualCameraType.ThirdPerson].Follow =
                    characterEntity.GetTransform().Value;
                //_mainCameraEntityFactory.Create(_rootGameObject.MainCamera);
            }

            return characterEntity;
        }
    }
}
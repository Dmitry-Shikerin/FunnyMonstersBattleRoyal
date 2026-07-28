using Fusion;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Characters.Presentation;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Input.Infrastructure;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Infrastructure
{
    public class CharacterFactory
    {
        private readonly RootGameObject _rootGameObject;
        private readonly CharacterEntityFactory _characterEntityFactory;
        private readonly InputEntityFactory _inputEntityFactory;

        public CharacterFactory(
            RootGameObject rootGameObject,
            CharacterEntityFactory characterEntityFactory,
            InputEntityFactory inputEntityFactory)
        {
            _rootGameObject = rootGameObject;
            _characterEntityFactory = characterEntityFactory;
            _inputEntityFactory = inputEntityFactory;
        }

        public NetworkObject Create(NetworkPrefabRef prefab, PlayerRef playerRef, NetworkRunner runner)
        {
            Vector3 position = _rootGameObject.CharacterSpawnPoint.position;
            Quaternion rotation = _rootGameObject.CharacterSpawnPoint.rotation;

            NetworkObject networkObject = runner.Spawn(prefab, position, rotation, playerRef);
            
            ProtoEntity inputEntity = _inputEntityFactory.Create(null);
            ProtoEntity characterEntity = _characterEntityFactory.Create(networkObject.GetComponent<EntityLink>());
            characterEntity.AddInputEntity(inputEntity);
            inputEntity.AddInputOwner(characterEntity);
            
            return networkObject;
        }
    }
}
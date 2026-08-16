using Fusion;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Hud.Presentations.Lobby;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Sources.EcsBoundedContexts.Cameras.Infrastructure;
using Sources.EcsBoundedContexts.Cameras.Presentation;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Input.Infrastructure;
using Sources.EcsBoundedContexts.Players.Infrastructure;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Infrastructure
{
    public class EcsCharacterFactory
    {
        private readonly PlayerEntityFactory _playerEntityFactory;
        private readonly IUiViewService _uiViewService;
        private readonly ISceneService _sceneService;
        private readonly MainCameraEntityFactory _mainCameraEntityFactory;
        private readonly RootGameObject _rootGameObject;
        private readonly CharacterEntityFactory _characterEntityFactory;
        private readonly InputEntityFactory _inputEntityFactory;

        public EcsCharacterFactory(
            PlayerEntityFactory playerEntityFactory,
            IUiViewService uiViewService,
            ISceneService sceneService,
            MainCameraEntityFactory mainCameraEntityFactory,
            RootGameObject rootGameObject,
            CharacterEntityFactory characterEntityFactory,
            InputEntityFactory inputEntityFactory)
        {
            _playerEntityFactory = playerEntityFactory;
            _uiViewService = uiViewService;
            _sceneService = sceneService;
            _mainCameraEntityFactory = mainCameraEntityFactory;
            _rootGameObject = rootGameObject;
            _characterEntityFactory = characterEntityFactory;
            _inputEntityFactory = inputEntityFactory;
        }

        public NetworkObject ServerCreate(NetworkPrefabRef prefab, PlayerRef playerRef, NetworkRunner runner)
        {
            //Create character and set input
            ProtoEntity spawnPointEntity = default;
            // Vector3 position = spawnPointEntity.GetSpawnPointTransform().Value.position;
            // Quaternion rotation = spawnPointEntity.GetSpawnPointTransform().Value.rotation;
            //position.y += 10;

            // NetworkObject networkObject = runner.Spawn(prefab, position, rotation, playerRef);
            // //spawnPointEntity.AddBusy();
            //
            // ProtoEntity inputEntity = _inputEntityFactory.Create(null);
            // ProtoEntity characterEntity = _characterEntityFactory.Create(networkObject.GetComponent<EntityLink>());
            // characterEntity.AddPlayerRef(playerRef);
            // characterEntity.AddInputEntity(inputEntity);
            // inputEntity.AddInputOwner(characterEntity);
            //
            // if (runner.LocalPlayer == playerRef)
            // {
            //     //Set camera follow transform
            //     _rootGameObject.MainCamera.GetModule<MainCameraModule>().Cameras[VirtualCameraType.ThirdPerson].Follow =
            //         characterEntity.GetTransform().Value;
            //     _mainCameraEntityFactory.Create(_rootGameObject.MainCamera);
            //     ProtoEntity playerEntity = _playerEntityFactory.LoadAndCreate();
            //     playerEntity.AddCharacterEntity(characterEntity);
            //     characterEntity.AddPlayerEntity(playerEntity);
            //
            //     //Init lobby ui
            //     if (_sceneService.CurrentSceneName == IdsConst.Lobby)
            //     {
            //         LobbyUiView lobbyView = _uiViewService.Get<LobbyUiView>();
            //         //_characterEntityFactory.InitLink(lobbyView.SkinChangersLink, characterEntity, false);
            //     }
            // }
            // else
            // {
            //     ProtoEntity playerEntity = _playerEntityFactory.Create();
            //     playerEntity.AddCharacterEntity(characterEntity);
            //     characterEntity.AddPlayerEntity(playerEntity);
            // }
            
            return null;
        }

        public ProtoEntity ClientCreate(NetworkObject networkObject, PlayerRef playerRef, NetworkRunner runner)
        {
            ProtoEntity inputEntity = _inputEntityFactory.Create(null);
            ProtoEntity characterEntity = _characterEntityFactory.Create(networkObject.GetComponent<EntityLink>());
            characterEntity.AddPlayerRef(playerRef);
            characterEntity.AddInputEntity(inputEntity);
            inputEntity.AddInputOwner(characterEntity);

            if (runner.LocalPlayer == playerRef)
            {
                //Set camera follow transform
                _rootGameObject.MainCamera.GetModule<MainCameraModule>().Cameras[VirtualCameraType.ThirdPerson].Follow =
                    characterEntity.GetTransform().Value;
                //_mainCameraEntityFactory.Create(_rootGameObject.MainCamera);
                ProtoEntity playerEntity = _playerEntityFactory.LoadAndCreate();
                playerEntity.AddCharacterEntity(characterEntity);
                characterEntity.AddPlayerEntity(playerEntity);
                
                //Init lobby ui
                if (_sceneService.CurrentSceneName == IdsConst.Lobby)
                {
                    LobbyUiView lobbyView = _uiViewService.Get<LobbyUiView>();
                    _characterEntityFactory.InitLink(lobbyView.PlayerNameLink, characterEntity, false);
                    //_characterEntityFactory.InitLink(lobbyView.SkinChangersLink, characterEntity, false);
                }
            }
            else
            {
                ProtoEntity playerEntity = _playerEntityFactory.Create();
                playerEntity.AddCharacterEntity(characterEntity);
                characterEntity.AddPlayerEntity(playerEntity);
            }

            return characterEntity;
        }
    }
}
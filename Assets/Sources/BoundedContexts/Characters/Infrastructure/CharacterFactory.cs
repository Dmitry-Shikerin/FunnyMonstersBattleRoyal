using Fusion;
using Reflex.Core;
using Sources.BoundedContexts.Characters.Presentation.Skins.Body;
using Sources.BoundedContexts.Characters.Presentation.Skins.BodyPart;
using Sources.BoundedContexts.Characters.Presentation.Skins.Eye;
using Sources.BoundedContexts.Characters.Presentation.Skins.Glove;
using Sources.BoundedContexts.Characters.Presentation.Skins.Head;
using Sources.BoundedContexts.Characters.Presentation.Skins.MounthandNoses;
using Sources.BoundedContexts.Characters.Presentation.Skins.Tail;
using Sources.BoundedContexts.Hud.Presentations.Lobby;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.BoundedContexts.Spawners.Presentation;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using Sources.Frameworks.ViewComponents.Presentation;
using UnityEngine;

namespace Sources.BoundedContexts.Characters.Infrastructure
{
    public class CharacterFactory
    {
        private readonly IUiViewService _uiViewService;
        private readonly ISceneService _sceneService;
        private readonly RootGameObject _rootGameObject;
        private readonly Container _container;

        public CharacterFactory(
            IUiViewService uiViewService,
            ISceneService sceneService,
            RootGameObject rootGameObject,
            Container container)
        {
            _uiViewService = uiViewService;
            _sceneService = sceneService;
            _rootGameObject = rootGameObject;
            _container = container;
        }

        public NetworkObject ServerCreate(NetworkPrefabRef prefabRef, PlayerRef playerRef, NetworkRunner runner)
        {
            //SpawnPoint
            SpawnPointView spawnPoint = _rootGameObject.GetAvailableSpawnPoint();
            Transform spawnTransform = spawnPoint.SpawnPositionTransform;
            spawnPoint.SetBusy(true);
            
            //Spawn
            NetworkObject networkObject = runner.Spawn(prefabRef, spawnTransform.position, spawnTransform.rotation, playerRef);
            
            //Init
            ViewComponentsLink viewComponentsLink = networkObject.GetComponent<ViewComponentsLink>();
            viewComponentsLink.Init(playerRef, _container);

            if (runner.LocalPlayer == playerRef)
            {
                //InitCamera
                _rootGameObject.Camera.SetFollow(VirtualCameraType.ThirdPerson, networkObject.transform);
                
                //Init lobby ui
                if (_sceneService.CurrentSceneName == IdsConst.Lobby)
                {
                    //Skin changers
                    ConstructSkinChangers(viewComponentsLink);
                }
            }

            return networkObject;
        }

        public void ClientCreate(NetworkObject playerValue, PlayerRef playerKey, NetworkRunner networkRunner)
        {
            throw new System.NotImplementedException();
        }

        private void ConstructSkinChangers(ViewComponentsLink link)
        {
            LobbyUiView lobbyView = _uiViewService.Get<LobbyUiView>();

            //Body
            BodySkinChangerUiView bodyUiSkinChanger = lobbyView.BodySkinChangerUiView;
            BodySkinChangerView bodySkinChanger = link.Get<BodySkinChangerView>();
            bodyUiSkinChanger.Construct(bodySkinChanger);
            bodySkinChanger.Construct(bodyUiSkinChanger);
            
            //BodyPart
            BodyPartSkinChangerUiView bodyPartUiSkinChangerUiView = lobbyView.BodyPartSkinChangerUiView;
            BodyPartSkinChangerView bodyPartSkinChangerView = link.Get<BodyPartSkinChangerView>();
            bodyPartUiSkinChangerUiView.Construct(bodyPartSkinChangerView);
            bodyPartSkinChangerView.Construct(bodyPartUiSkinChangerUiView);
            
            //Eye
            EyeSkinChangerUiView eyeSkinChangerUiView = lobbyView.EyeSkinChangerUiView;
            EyeSkinChangerView eyeSkinChangerView = link.Get<EyeSkinChangerView>();
            eyeSkinChangerUiView.Construct(eyeSkinChangerView);
            eyeSkinChangerView.Construct(eyeSkinChangerUiView);
            
            //Glove
            GloveSkinChangerUiView gloveSkinChangerUiView = lobbyView.GloveSkinChangerUiView;
            GloveSkinChangerView gloveSkinChangerView = link.Get<GloveSkinChangerView>();
            gloveSkinChangerUiView.Construct(gloveSkinChangerView);
            gloveSkinChangerView.Construct(gloveSkinChangerUiView);
            
            //Head
            HeadSkinChangerUiView headSkinChangerUiView = lobbyView.HeadSkinChangerUiView;
            HeadSkinChangerView headSkinChangerView = link.Get<HeadSkinChangerView>();
            headSkinChangerUiView.Construct(headSkinChangerView);
            headSkinChangerView.Construct(headSkinChangerUiView);
            
            //MouthandNoses
            MouthandNosesSkinChangerUiView mouthandNosesSkinChangerUiView = lobbyView.MouthandNosesSkinChangerUiView;
            MouthandNosesSkinChangerView mouthandNosesSkinChangerView = link.Get<MouthandNosesSkinChangerView>();
            mouthandNosesSkinChangerUiView.Construct(mouthandNosesSkinChangerView);
            mouthandNosesSkinChangerView.Construct(mouthandNosesSkinChangerUiView);
            
            //Tail
            TailSkinChangerUiView tailSkinChangerUiView = lobbyView.TailSkinChangerUiView;
            TailSkinChangerView tailSkinChangerView = link.Get<TailSkinChangerView>();
            tailSkinChangerUiView.Construct(tailSkinChangerView);
            tailSkinChangerView.Construct(tailSkinChangerUiView);
        }
    }
}
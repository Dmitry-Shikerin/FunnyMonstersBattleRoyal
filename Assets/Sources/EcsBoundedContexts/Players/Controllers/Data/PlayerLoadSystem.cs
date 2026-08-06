using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.BoundedContexts.Hud.Presentations.Lobby;
using Sources.BoundedContexts.Hud.Presentations.MainMenu;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Players.Domain.Data;
using Sources.EcsBoundedContexts.Players.Infrastructure;
using Sources.EcsBoundedContexts.Players.Presentation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;

namespace Sources.EcsBoundedContexts.Players.Controllers.Data
{
    [EcsSystem(10)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.MainMenu, AspectName.Game, AspectName.Lobby)]
    public class PlayerLoadSystem : IProtoInitSystem
    {
        private readonly ISceneService _sceneService;
        private readonly IUiViewService _uiViewService;
        private readonly PlayerEntityFactory _factory;
        private readonly IDataService _dataService;

        public PlayerLoadSystem(
            ISceneService sceneService,
            IUiViewService uiViewService,
            PlayerEntityFactory factory,
            IDataService dataService)
        {
            _sceneService = sceneService;
            _uiViewService = uiViewService;
            _factory = factory;
            _dataService = dataService;
        }
        
        public void Init(IProtoSystems systems)
        {
            if (_sceneService.CurrentSceneName == IdsConst.MainMenu)
            {
                LoadMainMenu();
                return;
            }
            
            LoadGameplay();
        }

        private void LoadGameplay()
        {
            EntityLink link = _sceneService.CurrentSceneName == IdsConst.Gameplay  ?
                _uiViewService.Get<GameplayUiView>().PlayerName :
                _uiViewService.Get<LobbyUiView>().PlayerNameLink;
            GameplayPlayerNameUiModule module = link.GetModule<GameplayPlayerNameUiModule>();
            
            //PlayerWallet
            ProtoEntity player = _factory.Create(link);
            
            if (_dataService.HasKey(IdsConst.Player) == false)
            {
                module.GeneratePlayerName();
                return;
            }
            
            //Load
            PlayerSaveData playerSaveData = _dataService.LoadData<PlayerSaveData>(IdsConst.Player);
            module.InitPlayerName(playerSaveData.Name);
            player.ReplacePlayerName(playerSaveData.Name);
        }

        private void LoadMainMenu()
        {
            EntityLink link = _uiViewService.Get<MainMenuUiView>().PlayerNameUiLink;
            PlayerNameUiModule module = link.GetModule<PlayerNameUiModule>();
            
            //PlayerWallet
            ProtoEntity player = _factory.Create(link);
            
            if (_dataService.HasKey(IdsConst.Player) == false)
            {
                module.GeneratePlayerName();
                return;
            }
            
            //Load
            PlayerSaveData playerSaveData = _dataService.LoadData<PlayerSaveData>(IdsConst.Player);
            module.InitPlayerName(playerSaveData.Name);
            player.ReplacePlayerName(playerSaveData.Name);
        }
    }
}
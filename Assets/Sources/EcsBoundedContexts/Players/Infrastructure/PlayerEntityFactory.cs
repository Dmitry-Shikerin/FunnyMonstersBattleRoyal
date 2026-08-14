using System;
using Leopotam.EcsProto;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Reflex.Core;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.BoundedContexts.Hud.Presentations.Lobby;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Players.Domain.Data;
using Sources.EcsBoundedContexts.Players.Presentation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.GameServices.Scenes.Services.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Factories;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Players.Infrastructure
{
    public class PlayerEntityFactory  : EntityFactory
    {
        private readonly IUiViewService _uiViewService;
        private readonly IDataService _dataService;
        private readonly ISceneService _sceneService;
        private readonly IEntityRepository _repository;

        public PlayerEntityFactory(
            IUiViewService uiViewService,
            IDataService dataService,
            ISceneService sceneService,
            IEntityRepository repository,
            ProtoWorld world,
            GameAspect aspect,
            Container container)
            : base(
                repository,
                world,
                aspect,
                container)
        {
            _uiViewService = uiViewService;
            _dataService = dataService;
            _sceneService = sceneService;
            _repository = repository;
        }

        public override ProtoEntity Create(EntityLink link)
        {
            Aspect.Player.NewEntity(out ProtoEntity entity);
            //_repository.AddByName(entity, IdsConst.Player);
            Authoring(link, entity);
            
            entity.AddStringId(IdsConst.Player);
            entity.AddPlayerName("DefaultPlayerName");
            entity.AddNetworkMain();
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();
            
            return entity;
        }

        public ProtoEntity Create()
        {
            Aspect.Player.NewEntity(out ProtoEntity entity);
            //_repository.AddByName(entity, IdsConst.Player);
            //Authoring(link, entity);
            
            entity.AddStringId(IdsConst.Player);
            entity.AddPlayerName("DefaultPlayerName");
            
            //Save
            entity.AddSavableData();
            entity.AddClearableData();

            return entity;
        }

        public ProtoEntity LoadAndCreate()
        {
            EntityLink link = _sceneService.CurrentSceneName == IdsConst.Gameplay  ?
                _uiViewService.Get<GameplayUiView>().PlayerName :
                _uiViewService.Get<LobbyUiView>().PlayerNameLink;
            GameplayPlayerNameUiModule module = link.GetModule<GameplayPlayerNameUiModule>();
            
            //PlayerWallet
            ProtoEntity player = Create(link);
            
            if (_dataService.HasKey(IdsConst.Player) == false)
                throw new InvalidOperationException("Player name not available");
            
            //Load
            PlayerSaveData playerSaveData = _dataService.LoadData<PlayerSaveData>(IdsConst.Player);
            module.InitPlayerName(playerSaveData.PlayerName);
            player.ReplacePlayerName(playerSaveData.PlayerName);

            return player;
        }
    }
}
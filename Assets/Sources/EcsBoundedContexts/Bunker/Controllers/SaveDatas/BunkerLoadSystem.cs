using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.EcsBoundedContexts.Bunker.Domain.Data;
using Sources.EcsBoundedContexts.Bunker.Infrastructure;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.Bunker.Controllers.SaveDatas
{
    [EcsSystem(16)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class BunkerLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly RootGameObject _rootGameObject;
        private readonly BunkerEntityFactory _bunkerEntityFactory;
        private readonly IDataService _dataService;

        public BunkerLoadSystem(
            IUiViewService uiViewService,
            RootGameObject rootGameObject,
            BunkerEntityFactory bunkerEntityFactory,
            IDataService dataService)
        {
            _uiViewService = uiViewService;
            _rootGameObject = rootGameObject;
            _bunkerEntityFactory = bunkerEntityFactory;
            _dataService = dataService;
        }

        public void Init(IProtoSystems systems)
        {
            //Create
            GameplayUiView gameplayUiView = _uiViewService.Get<GameplayUiView>();
            ProtoEntity bunkerEntity = _bunkerEntityFactory.Create(_rootGameObject.Bunker);
            _bunkerEntityFactory.InitUiLink(gameplayUiView.Bunker, bunkerEntity);

            if (_dataService.HasKey(IdsConst.Bunker) == false)
                return;
            
            //Bunker
            BunkerSaveData bunkerSaveData = _dataService.LoadData<BunkerSaveData>(IdsConst.Bunker);
            bunkerEntity.ReplaceHealth(bunkerSaveData.Health);
        }
    }
}
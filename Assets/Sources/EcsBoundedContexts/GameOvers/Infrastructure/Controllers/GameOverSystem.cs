using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;

namespace Sources.EcsBoundedContexts.GameOvers.Infrastructure.Controllers
{
    [EcsSystem(68)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class GameOverSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;
        private readonly ISdkService _sdkService;
        private ProtoEntity _bunker;
        private bool _isDeath;

        private ProtoEntity _killEnemyCounter;

        public GameOverSystem(
            IUiViewService uiViewService,
            IEntityRepository entityRepository,
            IStorageService storageService,
            ISdkService sdkService)
        {
            _uiViewService = uiViewService;
            _entityRepository = entityRepository;
            _storageService = storageService;
            _sdkService = sdkService;
        }

        public void Run()
        {
            // if (_bunker.HasHealth())
            //     return;
            //
            // OnDeath();
        }

        public void Init(IProtoSystems systems)
        {
            //_killEnemyCounter = _entityRepository.GetByName(IdsConst.KillEnemyCounter);
            //_bunker = _entityRepository.GetByName(IdsConst.Bunker);
        }

        private void OnDeath()
        {
            if (_isDeath)
                return;

            int score = _killEnemyCounter.GetKillEnemyCounter().Value;
            _sdkService.SetPlayerScore(score);
            _storageService.ClearAll();
            _uiViewService.Show(UiViewId.GameOverView);
            _uiViewService.Get<GameOverUiView>().ScoreText.text = score.ToString();
            _isDeath = true;
        }
    }
}
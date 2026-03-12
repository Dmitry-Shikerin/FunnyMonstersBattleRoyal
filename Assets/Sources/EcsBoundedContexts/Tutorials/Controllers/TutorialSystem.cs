using System;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.Curtains.Presentation.Interfaces;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Pauses;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Tutorials.Controllers
{
    [EcsSystem(80)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class TutorialSystem : IProtoInitSystem
    {
        private readonly ICurtainView _curtainView;
        private readonly IUiViewService _uiViewService;
        private readonly IEntityRepository _entityRepository;
        private readonly IStorageService _storageService;
        private readonly IPauseService _pauseService;
        
        private ProtoEntity _tutorial;

        public TutorialSystem(
            ICurtainView curtainView,
            IUiViewService uiViewService,
            IEntityRepository entityRepository,
            IStorageService storageService,
            IPauseService pauseService)
        {
            _curtainView = curtainView;
            _uiViewService = uiViewService;
            _entityRepository = entityRepository ?? throw new ArgumentNullException(nameof(entityRepository));
            _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
            _pauseService = pauseService;
        }

        public async void Init(IProtoSystems systems)
        {
            _tutorial = _entityRepository.GetByName(IdsConst.Tutorial);
            
            if (_tutorial.HasComplete())
                return;

            //Todo добавить сюда трай кетч
            await UniTask.WaitWhile(() => _curtainView.IsInProgress);
            _uiViewService.Show(UiViewId.Tutorial);
        }
    }
}
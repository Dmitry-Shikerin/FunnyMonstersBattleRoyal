using MyDependencies.Sources.Attributes;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class ClearSavesUiAction : UiAction
    {
        private IStorageService _storageService;

        public override UiActionId Id => UiActionId.ClearSaves;

        [Inject]
        private void Construct(IStorageService storageService) =>
            _storageService = storageService;

        public override void Handle() =>
            _storageService.ClearAll();
    }
}
using MyDependencies.Sources.Attributes;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class SaveVolumeUiAction : UiAction
    {
        private IStorageService _storageService;

        public override UiActionId Id => UiActionId.SaveVolume;

        [Inject]
        private void Construct(IStorageService storageService) =>
            _storageService = storageService;

        public override void Handle()
        {
            _storageService.Save(IdsConst.SoundsVolume);
            _storageService.Save(IdsConst.MusicVolume);
        }
    }
}
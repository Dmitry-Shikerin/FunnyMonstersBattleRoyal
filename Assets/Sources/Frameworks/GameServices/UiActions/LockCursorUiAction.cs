using Reflex.Attributes;
using Sources.BoundedContexts.Camera.Presentation;
using Sources.BoundedContexts.Cursors.Services;
using Sources.BoundedContexts.RootGameObjects.Presentation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class LockCursorUiAction : UiAction
    {
        private ThirdPersonCameraView _view;
        private ICursorService _cursorService;

        public override UiActionId Id => UiActionId.LockCursor;
        
        [Inject]
        private void Construct(
            ICursorService cursorService,
            RootGameObject rootGameObject)
        {
            _cursorService = cursorService;
            _view = rootGameObject.MainCamera.ThirdPersonCamera;
        }

        public override void Handle()
        {
            _cursorService.LockCursor();
            _view.UnlockCameraRotation();
        }
    }
}
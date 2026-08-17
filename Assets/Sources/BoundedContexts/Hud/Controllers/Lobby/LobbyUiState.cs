using System.Collections.Generic;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.States;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;

namespace Sources.BoundedContexts.Hud.Controllers.Lobby
{
    [Category(NcCategoriesConst.Ui)]
    public class LobbyUiState : UiViewStateBase
    {
        protected override bool IsHideAllViewsEntered { get; } = true;
        protected override bool IsHideAllPopUpsEntered { get; } = true;
        
        protected override void AddEnterShowedViews(List<UiViewId> viewIds)
        {
            viewIds.Add(UiViewId.Lobby);
        }
        
        protected override void AddEnterUiActions(List<UiActionId> uiActionIds)
        {
            uiActionIds.Add(UiActionId.UnlockCursor);
        }
    }
}
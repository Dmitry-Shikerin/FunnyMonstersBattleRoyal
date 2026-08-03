using System.Collections.Generic;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.States;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;

namespace Sources.BoundedContexts.Hud.Controllers.MainMenu
{
    [Category(NcCategoriesConst.Ui)]
    public class PartyUiState : UiViewStateBase
    {
        protected override bool IsHideAllPopUpsEntered { get; }
        protected override bool IsHideAllPopUpsExited { get; }
        protected override bool IsHideAllViewsEntered { get; } = true;
        protected override bool IsHideAllViewsExited { get; }
        
        protected override void AddEnterShowedViews(List<UiViewId> viewIds)
        {
            viewIds.Add(UiViewId.Party);
            viewIds.Add(UiViewId.HudBackground);
        }

        protected override void AddEnterHidedViews(List<UiViewId> viewIds)
        {
        }

        protected override void AddExitShowViews(List<UiViewId> viewIds)
        {
        }

        protected override void AddExitHidedViews(List<UiViewId> viewIds)
        {
        }

        protected override void AddEnterUiActions(List<UiActionId> uiActionIds)
        {
        }

        protected override void AddExitUiActions(List<UiActionId> uiActionIds)
        {
        }
    }
}
using System.Collections.Generic;
using ParadoxNotion.Design;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.States;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;

namespace Sources.BoundedContexts.Hud.Controllers.Gameplay
{
    [Category(NcCategoriesConst.Ui)]
    public class GameplayUiState : UiViewStateBase
    {
        protected override bool IsHideAllViewsEntered { get; } = true;

        protected override void AddEnterShowedViews(List<UiViewId> viewIds)
        {
            viewIds.Add(UiViewId.Gameplay);
        }
    }
}
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using Sources.Frameworks.YandexSdkFramework.Leaderboards.Presentations.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class LeaderboardUiView : UiView
    {
        [field: Required] [field: SerializeField] public List<LeaderBoardElementView> LeaderboardElements { get; private set; }
    }
}
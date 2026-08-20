using Sirenix.OdinInspector;
using Sources.BoundedContexts.Players.Presentation.Ui;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Gameplay
{
    public class GameplayUiView : UiView
    {
        [field: Required] [field: SerializeField] public PlayerNameUiView PlayerName { get; private set; }
    }
}
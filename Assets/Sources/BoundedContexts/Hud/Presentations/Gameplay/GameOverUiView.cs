using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Gameplay
{
    public class GameOverUiView : UiView
    {
        [field: Required] [field: SerializeField] public TMP_Text ScoreText { get; private set; }
    }
}
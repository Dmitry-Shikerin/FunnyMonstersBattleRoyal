using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using TMPro;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Gameplay
{
    public class InterstitialAfterWaveUiView : UiView
    {
        [field: SerializeField] public TMP_Text TimerText { get; private set; }
    }
}
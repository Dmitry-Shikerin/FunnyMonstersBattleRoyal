using Sirenix.OdinInspector;
using Sources.BoundedContexts.Settings.Presentation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Common
{
    public class SettingsUiView : UiView
    {
        [field: Required] [field: SerializeField] public SettingsView SettingsView { get; private set; }
    }
}
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class MainMenuSettingsUiView : UiView
    {
        [field: SerializeField] public EntityLink SettingsLink { get; private set; }
    }
}
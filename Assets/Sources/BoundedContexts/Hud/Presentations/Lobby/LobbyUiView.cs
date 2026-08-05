using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Lobby
{
    public class LobbyUiView : UiView
    {
        [field: Required] [field: SerializeField] public EntityLink PlayerNameLink { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink SkinChangersLink { get; private set; }
    }
}
using Sirenix.OdinInspector;
using Sources.BoundedContexts.Networks;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class CreateRoomUiView : UiView
    {
        [field: Required] [field: SerializeField] public RoomsView RoomView { get; private set; }
    }
}
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Curtains.Implementation;
using Sources.Frameworks.DeepFramework.DeepUtils.ConfigCollectors.Domain.ScriptableObjects;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(UiConfig), menuName = "Configs/" + nameof(UiConfig), order = 51)]
    public class UiConfig : Config
    {
        //TODO сделать загрузку отдельной
        [field: SerializeField] public UiManagerConfig GameUiConfig { get; private set; }
        [field: SerializeField] public UiManagerConfig MainMenuUiConfig { get; private set; }
        [field: SerializeField] public UiManagerConfig LobbyUiConfig { get; private set; }
        [field: SerializeField] public CurtainView CurtainView { get; private set; }
    }
}
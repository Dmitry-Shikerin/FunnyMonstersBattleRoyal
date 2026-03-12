using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.BoundedContexts.Hud.Presentations.MainMenu
{
    public class MainHudUiView : UiView
    {
        [field: Required] [field: SerializeField] public EntityLink DailyReward { get; private set; }        
        [field: Required] [field: SerializeField] public Button LoadGameButton { get; private set; }        
        [field: Required] [field: SerializeField] public EntityLink HealthBuster { get; private set; }
    }
}
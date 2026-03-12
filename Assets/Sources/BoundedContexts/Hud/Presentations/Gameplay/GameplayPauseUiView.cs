using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Gameplay
{
    public class GameplayPauseUiView : UiView
    {
        [field: SerializeField] public EntityLink DailyReward { get; set; }
    }
}
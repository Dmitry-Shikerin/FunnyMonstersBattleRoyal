using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Gameplay
{
    public class AchievementUiPopUpView : UiPopUpView
    {
        [field: Required] [field: SerializeField] public EntityLink AchievementLink { get; private set; }
    }
}
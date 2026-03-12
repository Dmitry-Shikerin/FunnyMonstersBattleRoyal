using System.Collections.Generic;
using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sirenix.OdinInspector;
using Sources.Frameworks.DeepFramework.DeepUiManager.Presentation.Implementation.Views;
using UnityEngine;

namespace Sources.BoundedContexts.Hud.Presentations.Common
{
    public class AchievementsUiView : UiView
    {
        [field: Required] [field: SerializeField] public List<EntityLink> Achievements { get; private set; }
        [field: Required] [field: SerializeField] public EntityLink AchievementInfo { get; private set; }
    }
}
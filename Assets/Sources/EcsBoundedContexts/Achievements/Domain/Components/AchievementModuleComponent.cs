using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Achievements.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Achievements.Domain.Components
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Achievements)]
    [Aspect(AspectName.MainMenu, AspectName.Game)]
    public struct AchievementModuleComponent
    {
        public AchievementModule Value;
    }
}
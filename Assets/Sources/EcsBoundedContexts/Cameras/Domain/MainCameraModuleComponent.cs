using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Cameras.Presentation;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;

namespace Sources.EcsBoundedContexts.Cameras.Domain
{
    [Serializable]
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Camera)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public struct MainCameraModuleComponent
    {
        public MainCameraModule Value;
    }
}
using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Unity.Cinemachine;

namespace Sources.EcsBoundedContexts.Cameras.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring]
    [Component(group: ComponentGroup.Camera)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public struct CinemachineCameraComponent
    {
        public CinemachineCamera Value;
    }
}
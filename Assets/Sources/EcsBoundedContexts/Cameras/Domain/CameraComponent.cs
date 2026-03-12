using System;
using Leopotam.EcsProto.Unity;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Cameras.Domain
{
    [Serializable] 
    [ProtoUnityAuthoring("Camera")]
    [Component(group: ComponentGroup.Camera)]
    [Aspect(AspectName.Game, AspectName.MainMenu)]
    public struct CameraComponent
    {
        public Camera Camera;
    }
}
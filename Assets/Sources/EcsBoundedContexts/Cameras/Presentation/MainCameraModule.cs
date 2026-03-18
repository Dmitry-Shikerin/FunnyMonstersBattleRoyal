using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using Sources.EcsBoundedContexts.Cameras.Domain;
using Unity.Cinemachine;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Cameras.Presentation
{
    public class MainCameraModule : EntityModule
    {
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public CinemachineBrain Brain { get; private set; }
        [field: SerializeField] public CinemachineCamerasDictionary Cameras { get; private set; }
    }
}
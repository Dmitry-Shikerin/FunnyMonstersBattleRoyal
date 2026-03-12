using System;
using Sources.Frameworks.DeepFramework.DeepUtils.Dictionaries;
using Unity.Cinemachine;

namespace Sources.EcsBoundedContexts.Cameras.Domain
{
    [Serializable]
    public class CinemachineCamerasDictionary : SerializedDictionary<VirtualCameraType, CinemachineCamera>
    {
    }
}
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.LookAt.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.LookAt.Controllers
{
    [EcsSystem(71)]
    [ComponentGroup(ComponentGroup.Light)]
    [Aspect(AspectName.Game)]
    public class LookAtCameraSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                LookAtComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                Transform transform = entity.GetLookAt().Value;
                
                Quaternion rotation = Camera.main.transform.rotation;
                transform.LookAt(transform.position + rotation * Vector3.back, rotation * Vector3.up);
            }
        }
    }
}
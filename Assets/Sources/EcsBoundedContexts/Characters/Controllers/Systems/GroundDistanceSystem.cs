using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Characters.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Characters.Controllers.Systems
{
    [EcsSystem(55)]
    [ComponentGroup(ComponentGroup.Characters)]
    [Aspect(AspectName.Game)]
    public class GroundDistanceSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                CharacterTag,
                GroundDistanceComponent>());
        
        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                Transform groundedCheck = entity.GetCharacterModule().Value.GroundCheck;
                LayerMask mask = entity.GetCharacterConfig().Value.GroundMask;
                
                Ray ray = new Ray(groundedCheck.position, Vector3.down);
                
                if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, mask) == false)
                    continue;
                
                entity.ReplaceGroundDistance(hit.distance);
            }
        }
    }
}
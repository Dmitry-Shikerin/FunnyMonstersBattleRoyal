using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Movements.Move.Components;
using Sources.EcsBoundedContexts.Movements.TargetPoint.Components;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.EcsBoundedContexts.Movements.Move.Systems
{
    [EcsSystem(75)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class NavMeshMoveSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _protoIt =
            new(It.Inc<
                TransformComponent,
                NavMeshComponent,
                TargetPointComponent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _protoIt)
            {
                NavMeshAgent agent = entity.GetNavMesh().Value;
                Vector3 targetPoint = entity.GetTargetPoint().Value;

                //TODO обратить внимание(но вроде работает)
                if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
                {
                    entity.DelTargetPoint();
                    agent.isStopped = true;
                    agent.ResetPath();
                    continue;
                }
                
                agent.SetDestination(targetPoint);
                
                float stoppingDistance = agent.stoppingDistance + 0.1f;

                if (Vector3.Distance(agent.destination, agent.transform.position) > stoppingDistance)
                    continue;
                    
                entity.DelTargetPoint();
                agent.isStopped = true;
                agent.ResetPath();
            }
        }
    }
}
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Components;
using Sources.EcsBoundedContexts.SwingingTrees.Domain.Jobs;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

namespace Sources.EcsBoundedContexts.SwingingTrees.Controllers.Systems
{
    [EcsSystem(77)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class TreeSwingerSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _it = new (
            It.Inc<
                SwingingTreeComponent, 
                TransformComponent>());

        private NativeArray<SwingingTreeComponent> _treeSwingers;
        private TransformAccessArray _transformAccess;

        public void Run()
        {
            int len = _it.Len();
            _treeSwingers = new NativeArray<SwingingTreeComponent>(len, Allocator.TempJob);
            _transformAccess = new TransformAccessArray(len);
            
            int index = len - 1;
            
            foreach (ProtoEntity entity in _it)
            {
                SwingingTreeComponent treeSwinger = entity.GetSwingingTree();
                ref TransformComponent treeTransform = ref entity.GetTransform();

                _treeSwingers[index] = treeSwinger;
                _transformAccess.Add(treeTransform.Value);

                index--;
            }

            TreeSwingJob job = new TreeSwingJob(_treeSwingers, Time.time);
            job.Schedule(_transformAccess).Complete();
            _treeSwingers.Dispose();
            _transformAccess.Dispose();
        }
    }
}
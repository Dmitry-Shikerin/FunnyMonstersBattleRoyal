using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.GameObjects.Domain;
using UnityEngine;

namespace Sources.EcsBoundedContexts.GameObjects.Controllers
{
    [EcsSystem(84)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class ActiveGameObjectSystem : IProtoRunSystem
    {
        [DI] private readonly ProtoIt _enableIt = new (
            It.Inc<
                GameObjectComponent,
                EnableGameObjectEvent>());
        [DI] private readonly ProtoIt _disableIt = new (
            It.Inc<
                GameObjectComponent,
                DisableGameObjectEvent>());

        public void Run()
        {
            foreach (ProtoEntity entity in _enableIt)
            {
                GameObject gameObject = entity.GetGameObject().Value;
                gameObject.SetActive(true);
                
                if (entity.HasActive())
                    continue;

                entity.AddActive();
            }      
            
            foreach (ProtoEntity entity in _disableIt)
            {
                GameObject gameObject = entity.GetGameObject().Value;
                gameObject.SetActive(false);
                entity.DelActive();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime
{
    public class EntityModule : MonoBehaviour
    {
        public ProtoEntity Entity { get; private set; }
        protected ProtoWorld World { get; private set; }
        public EntityLink Link { get; private set; }
        protected List<IDisposable> Disposables { get; private set; } = new ();
        protected bool IsInitialized { get; private set; }

        public void Initialize(
            ProtoEntity entity, 
            ProtoWorld world,
            EntityLink link)
        {
            Entity = entity;
            World = world;
            Link = link;
            IsInitialized = true;
        }

        private void OnDisable()
        {
            foreach (IDisposable disposable in Disposables)
                disposable?.Dispose();
            
            OnAfterDisable();
        }

        protected virtual void OnAfterDisable()
        {
        }
    }
}
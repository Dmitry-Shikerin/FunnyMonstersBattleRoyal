using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.GameServices.EntityPools.Implementation.Bakers;
using Sources.Frameworks.GameServices.EntityPools.Interfaces.Bakers.Generic;
using Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.GameServices.EntityPools.Implementation
{
    public class EntityPool<T> : IEntityPool
        where T : struct
    {
        private readonly Transform _poolRoot;
        private readonly IPoolBaker<T> _baker;
        private readonly List<ProtoEntity> _pool = new();
        private readonly List<ProtoEntity> _freedoms = new();
        private Func<ProtoEntity> _createFunc;

        public EntityPool(
            Transform root)
        {
            _baker = new PoolBaker<T>(root);
            _poolRoot = new GameObject($"Pool of {typeof(T).Name}").transform;
            _poolRoot.SetParent(root.transform);
        }
        
        public IList<ProtoEntity> Freedoms => _freedoms;
        public IReadOnlyList<ProtoEntity> Pool => _pool;

        public bool TryGet(out ProtoEntity entity)
        {
            if (_createFunc == null)
                throw new InvalidOperationException("Pool is not initialized");
            
            ProtoEntity pooledEntity = Get();
            
            if (pooledEntity != default)
            {
                entity = pooledEntity;

                return true;
            }

            entity = default;

            return false;
        }

        public void InitPool(Func<ProtoEntity> createFunc)
        {
            if(_createFunc != null)
                throw new InvalidOperationException("Pool is already initialized");
            
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
        }

        public ProtoEntity Get()
        {
            if (_createFunc == null)
                throw new InvalidOperationException("Pool is not initialized");
            
            if (_pool.Count > 0)
            {
                ProtoEntity pooledEntity = _pool.First();
                _freedoms.Add(pooledEntity);
                pooledEntity.DelInPool();
                pooledEntity.AddEnableGameObjectEvent();
                _pool.Remove(pooledEntity);
                _baker.Add(pooledEntity.GetTransform().Value);

                return pooledEntity;
            }
            
            ProtoEntity newEntity = _createFunc.Invoke();
            _freedoms.Add(newEntity);
            _baker.Add(newEntity.GetTransform().Value);
            newEntity.AddReturnToPoolAction(() => Return(newEntity));
            
            return newEntity;
        }

        public void Return(ProtoEntity entity)
        {
            if (_createFunc == null)
                throw new InvalidOperationException("Pool is not initialized");
            
            if (_pool.Contains(entity))
                throw new InvalidOperationException("This entity is already in pool");

            entity.GetTransform().Value.SetParent(_poolRoot);
            _pool.Add(entity);
            _freedoms.Remove(entity);
            entity.AddInPool();
            entity.AddDisableGameObjectEvent();
        }

        public bool Contains(ProtoEntity entity)
        {
            if (_createFunc == null)
                throw new InvalidOperationException("Pool is not initialized");
            
            return _pool.Contains(entity);
        }
    }
}
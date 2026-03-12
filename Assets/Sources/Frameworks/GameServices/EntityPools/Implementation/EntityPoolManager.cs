using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.Frameworks.GameServices.EntityPools.Domain.Configs;
using Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces;
using UnityEngine;

namespace Sources.Frameworks.GameServices.EntityPools.Implementation
{
    public class EntityPoolManager : IEntityPoolManager
    {
        private readonly Transform _root = new GameObject("Root of Pools").transform;
        private readonly Dictionary<Type, IEntityPool> _pools = new();

        private PoolManagerCollector _poolManagerCollector;

        public ProtoEntity Get<T>()
            where T : struct =>
            GetPool<T>().Get();

        public IEntityPool GetPool<T>()
            where T : struct
        {
            if (_pools.ContainsKey(typeof(T)) == false)
                _pools[typeof(T)] = new EntityPool<T>(_root);

            return _pools[typeof(T)];
        }

        public bool Contains<T>(ProtoEntity entity)
            where T : struct =>
            (_pools[typeof(T)].Contains(entity));
    }
}
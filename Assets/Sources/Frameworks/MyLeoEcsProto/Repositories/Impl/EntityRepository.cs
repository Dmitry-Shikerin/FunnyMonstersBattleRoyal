using System;
using System.Collections.Generic;
using Leopotam.EcsProto;
using UnityEngine;

namespace Sources.Frameworks.MyLeoEcsProto.Repositories.Impl
{
    public class EntityRepository : IEntityRepository
    {
        private readonly ProtoWorld _world;
        private readonly Dictionary<string, ProtoEntity> _entitiesGoHash = new();
        private readonly Dictionary<string, ProtoEntity> _entitiesNames = new();

        public EntityRepository(ProtoWorld world)
        {
            _world = world;
        }

        public void AddByName(ProtoEntity entity, string id)
        {
            if (_entitiesNames.TryAdd(id, entity) == false)
                throw new InvalidOperationException("Can't add entity by name: " + id);
        }

        public ProtoEntity GetByName(string id)
        {
            if (_entitiesNames.TryGetValue(id, out ProtoEntity entity) == false)
                throw new KeyNotFoundException("Can't find entity by name: " + id);
            
            return entity;
        }

        public void AddBayHash(ProtoEntity entity, GameObject gameObject)
        {
            string hashCode = gameObject.GetHashCode().ToString();
            
            if (_entitiesGoHash.TryAdd(hashCode, entity) == false)
                throw new InvalidOperationException();
        }

        public bool TryGetByHash<T>(GameObject gameObject, out T component)
            where T : struct
        {
            if (HasByHash<T>(gameObject) == false)
            {
                component = default;
                return false;
            }

            IProtoPool pool = _world.Pool(typeof(T));

            if (pool is not ProtoPool<T> concrete)
                throw new InvalidCastException();

            ProtoEntity entity = _entitiesGoHash[gameObject.GetHashCode().ToString()];
            component = concrete.Get(entity);
            
            return true;
        }

        public bool TryGetByName(string id, out ProtoEntity entity) =>
            _entitiesNames.TryGetValue(id, out entity);

        public bool TryGet(GameObject gameObject, out ProtoEntity entity)
        {
            if (HasByHash(gameObject) == false)
            {
                entity = default;
                return false;
            }

            entity = _entitiesGoHash[gameObject.GetHashCode().ToString()];
            
            return true;
        }

        public bool HasByHash<T>(GameObject gameObject)
            where T : struct
        {
            string hashCode = gameObject.GetHashCode().ToString();

            if (_entitiesGoHash.TryGetValue(hashCode, out ProtoEntity entity) == false)
                return false;

            return HasByHash<T>(entity);
        }

        public bool HasByHash(GameObject gameObject)
        {
            string hashCode = gameObject.GetHashCode().ToString();

            return _entitiesGoHash.ContainsKey(hashCode);
        }

        private bool HasByHash<T>(ProtoEntity entity)
            where T : struct
        {
            IProtoPool pool = _world.Pool(typeof(T));

            return pool.Has(entity);
        }
    }
}
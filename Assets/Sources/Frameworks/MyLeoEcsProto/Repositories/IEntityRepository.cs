using Leopotam.EcsProto;
using UnityEngine;

namespace Sources.Frameworks.MyLeoEcsProto.Repositories
{
    public interface IEntityRepository
    {
        void AddByName(ProtoEntity entity, string id);
        ProtoEntity GetByName(string id);
        void AddBayHash(ProtoEntity entity, GameObject gameObject);
        bool TryGetByHash<T>(GameObject gameObject, out T component)
            where T : struct;        
        bool TryGetByName(string id, out ProtoEntity entity);
        bool TryGet(GameObject gameObject, out ProtoEntity entity);
        bool HasByHash<T>(GameObject gameObject)
            where T : struct;
        bool HasByHash(GameObject gameObject);
    }
}
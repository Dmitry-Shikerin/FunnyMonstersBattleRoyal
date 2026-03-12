using System;
using System.Collections.Generic;
using Leopotam.EcsProto;

namespace Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces
{
    public interface IEntityPool
    {
        IList<ProtoEntity> Freedoms { get; }
        IReadOnlyList<ProtoEntity> Pool { get; }

        void InitPool(Func<ProtoEntity> createFunc);
        bool TryGet(out ProtoEntity entity);
        ProtoEntity Get();
        void Return(ProtoEntity entity);
        bool Contains(ProtoEntity entity);
    }
}
using Leopotam.EcsProto;

namespace Sources.Frameworks.MyLeoEcsProto.ObjectPools.Interfaces
{
    public interface IEntityPoolManager
    {
        public ProtoEntity Get<T>()
            where T : struct;
        public IEntityPool GetPool<T>()
            where T : struct;
        public bool Contains<T>(ProtoEntity entity)
            where T : struct;
    }
}
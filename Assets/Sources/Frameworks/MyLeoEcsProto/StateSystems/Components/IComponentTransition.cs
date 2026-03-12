using Leopotam.EcsProto;

namespace Sources.Frameworks.MyLeoEcsProto.StateSystems.Components
{
    public interface IComponentTransition
    {
        void Transit(ProtoEntity entity);
        bool CanTransit(ProtoEntity entity);
    }
}


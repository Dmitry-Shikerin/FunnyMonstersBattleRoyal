using Leopotam.EcsProto;

namespace Sources.Frameworks.MyLeoEcsProto.Features
{
    public interface IEcsFeature : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
    {
        void Enable();
        void Disable();
    }
}
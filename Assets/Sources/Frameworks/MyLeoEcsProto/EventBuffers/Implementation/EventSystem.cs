using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Interfaces;

namespace Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation
{
    public abstract class EventSystem<T> : IProtoRunSystem
        where T : struct, IEvent
    {
        protected abstract ProtoPool<T> Pool { get; }
        protected abstract ProtoIt Iterator { get; }

        public void Run()
        {
            foreach (ProtoEntity entity in Iterator)
            {
                ref T @event = ref Pool.Get(entity);

                Receive(ref @event);
                Pool.Del(entity);
            }
        }

        protected abstract void Receive(ref T @event);
    }
}
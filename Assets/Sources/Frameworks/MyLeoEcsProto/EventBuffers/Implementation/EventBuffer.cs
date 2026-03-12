using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.MyLeoEcsProto.EventBuffers.Interfaces;

namespace Sources.Frameworks.MyLeoEcsProto.EventBuffers.Implementation
{
    public class EventBuffer : IEventBuffer
    {
        private readonly ProtoEntity _entity;

        public EventBuffer(GameAspect aspect)
        {
            aspect.EventBuffer.NewEntity(out ProtoEntity entity);
            _entity = entity;
        }

        public void Send<T>(T @event)
            where T : struct, IEvent
        {
            //_entity.Add(@event);
            
        }
    }
}
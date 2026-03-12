namespace Sources.Frameworks.MyLeoEcsProto.EventBuffers.Interfaces
{
    public interface IEventBuffer
    {
        void Send<T>(T @event)
            where T : struct, IEvent;
    }
}
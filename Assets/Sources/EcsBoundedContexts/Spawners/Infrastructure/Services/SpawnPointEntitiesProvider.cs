using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Spawners.Domain;

namespace Sources.EcsBoundedContexts.Spawners.Infrastructure.Services
{
    public class SpawnPointEntitiesProvider
    {
        [DI] private readonly ProtoItExc _it = new (
            It.Inc<SpawnPointTag>(),
            It.Exc<BusyComponent>());

        public ProtoEntity GetFreedomPoint() =>
            _it.First().Entity;
    }
}
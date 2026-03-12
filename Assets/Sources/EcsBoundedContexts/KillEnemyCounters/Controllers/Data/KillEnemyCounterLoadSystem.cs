using Leopotam.EcsProto;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.KillEnemyCounters.Domain.Data;
using Sources.EcsBoundedContexts.KillEnemyCounters.Infrastructure;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.KillEnemyCounters.Controllers.Data
{
    [EcsSystem(12)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class KillEnemyCounterLoadSystem : IProtoInitSystem
    {
        private readonly IDataService _dataService;
        private readonly IEntityRepository _entityRepository;
        private readonly KillEnemyCounterEntityFactory _killEnemyCounterEntityFactory;

        public KillEnemyCounterLoadSystem(
            IEntityRepository entityRepository,
            IDataService dataService,
            KillEnemyCounterEntityFactory killEnemyCounterEntityFactory)
        {
            _entityRepository = entityRepository;
            _dataService = dataService;
            _killEnemyCounterEntityFactory = killEnemyCounterEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            //Create
            _killEnemyCounterEntityFactory.Create(null);

            if (_dataService.HasKey(IdsConst.KillEnemyCounter) == false)
                return;

            //Load
            KillEnemyCounterSaveData data = _dataService.LoadData<KillEnemyCounterSaveData>(IdsConst.KillEnemyCounter);
            _entityRepository
                .GetByName(IdsConst.KillEnemyCounter)
                .ReplaceKillEnemyCounter(data.KilledEnemiesCount);
        }
    }
}
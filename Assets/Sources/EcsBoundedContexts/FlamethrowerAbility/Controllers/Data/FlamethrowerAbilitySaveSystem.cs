using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.ApplyAbility.Domain;
using Sources.EcsBoundedContexts.ApplyAbility.Domain.Data;
using Sources.EcsBoundedContexts.Common.Domain.Components;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.SaveLoads.Domain;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;

namespace Sources.EcsBoundedContexts.FlamethrowerAbility.Controllers.Data
{
    [EcsSystem(508)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class FlamethrowerAbilitySaveSystem : IProtoRunSystem
    {
        private readonly IDataService _dataService;

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                AbilityTag,
                SaveDataEvent,
                StringIdComponent>());

        public FlamethrowerAbilitySaveSystem(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                string id = entity.GetStringId().Value;

                if (id != IdsConst.FlamethrowerAbility)
                    continue;

                AbilitySaveData data = new AbilitySaveData
                {
                    Id = IdsConst.FlamethrowerAbility,
                    IsFirstUsedCompleted = entity.HasFirstUsedCompleted(),
                };

                _dataService.SaveData(data, IdsConst.FlamethrowerAbility);
            }
        }
    }
}
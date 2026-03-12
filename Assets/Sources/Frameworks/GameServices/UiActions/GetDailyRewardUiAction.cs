using Leopotam.EcsProto;
using MyDependencies.Sources.Attributes;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.Frameworks.DeepFramework.DeepUiManager.Controllers.Implementation.UiActions;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.Frameworks.GameServices.UiActions
{
    public class GetDailyRewardUiAction : UiAction
    {
        private IEntityRepository _entityRepository;
        
        public override UiActionId Id => UiActionId.GetDailyReward;

        [Inject]
        private void Construct(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }
        
        public override void Handle()
        {
            ProtoEntity dailyReWard = _entityRepository.GetByName(IdsConst.HealthBooster);
            dailyReWard.AddIncreaseEvent();
            dailyReWard.AddSaveDataEvent();
        }
    }
}
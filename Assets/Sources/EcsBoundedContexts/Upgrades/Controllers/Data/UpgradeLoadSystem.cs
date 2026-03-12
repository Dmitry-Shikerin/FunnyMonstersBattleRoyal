using System.Collections.Generic;
using Leopotam.EcsProto;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Upgrades.Domain.Components;
using Sources.EcsBoundedContexts.Upgrades.Domain.Data;
using Sources.EcsBoundedContexts.Upgrades.Infrastructure;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces.Data;
using Sources.Frameworks.MyLeoEcsProto.Repositories;

namespace Sources.EcsBoundedContexts.Upgrades.Controllers.Data
{
    [EcsSystem(8)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class UpgradeLoadSystem : IProtoInitSystem
    {
        private readonly IUiViewService _uiViewService;
        private readonly IEntityRepository _entityRepository;
        private readonly IDataService _dataService;
        private readonly UpgradeEntityFactory _upgradeEntityFactory;

        public UpgradeLoadSystem(
            IUiViewService uiViewService,
            IEntityRepository entityRepository,
            IDataService dataService,
            UpgradeEntityFactory upgradeEntityFactory)
        {
            _uiViewService = uiViewService;
            _entityRepository = entityRepository;
            _dataService = dataService;
            _upgradeEntityFactory = upgradeEntityFactory;
        }

        public void Init(IProtoSystems systems)
        {
            GameplayUiView gameplayUiView = _uiViewService.Get<GameplayUiView>();

            //Create
            _upgradeEntityFactory.Create(gameplayUiView.NukeAbilityUpgrade, IdsConst.NukeUpgrade);
            _upgradeEntityFactory.Create(gameplayUiView.FlamethrowerAbilityUpgrade, IdsConst.FlamethrowerUpgrade);
            _upgradeEntityFactory.Create(gameplayUiView.HealthUpgrade, IdsConst.HealthUpgrade);
            _upgradeEntityFactory.Create(gameplayUiView.AttackUpgrade, IdsConst.AttackUpgrade);

            IReadOnlyList<string> ids = IdsConst.GetIds<UpgradeSaveData>();

            foreach (string id in ids)
            {
                if (_dataService.HasKey(id) == false)
                    return;
            }

            //Load
            foreach (string id in ids)
            {
                UpgradeSaveData attackUpgradeData = _dataService.LoadData<UpgradeSaveData>(id);
                ProtoEntity entity = _entityRepository.GetByName(id);
                ref UpgradeConfigComponent attackUpgradeComponent = ref entity.GetUpgradeConfig();
                attackUpgradeComponent.Index = attackUpgradeData.UpgradeIndex;
            }
        }
    }
}
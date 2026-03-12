using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.Upgrades.Domain.Components;
using Sources.EcsBoundedContexts.Upgrades.Domain.Configs;
using Sources.EcsBoundedContexts.Upgrades.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using TMPro;

namespace Sources.EcsBoundedContexts.Upgrades.Controllers
{
    [EcsSystem(50)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class UpgradeSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IEntityRepository _repository;

        [DI] private readonly ProtoIt _it = new(
            It.Inc<
                ApplyUpgradeEvent,
                UpgradeConfigComponent>());
        [DI] private readonly ProtoIt _initIt = new(
            It.Inc<
                UpgradeTag>());

        private ProtoEntity _wallet;

        public UpgradeSystem(IEntityRepository repository)
        {
            _repository = repository;
        }

        public void Init(IProtoSystems systems)
        {
            _wallet = _repository.GetByName(IdsConst.PlayerWallet);
            
            foreach (ProtoEntity entity in _initIt)
                UpdateNextUpgradePriceText(entity);
        }

        public void Run()
        {
            foreach (var entity in _it)
            {
                ref UpgradeConfigComponent upgradeConfigComponent = ref entity.GetUpgradeConfig();
                int coins = _wallet.GetPlayerWallet().Value;

                if (upgradeConfigComponent.Index == upgradeConfigComponent.Value.Levels.Count - 1)
                    continue;
                
                int price = upgradeConfigComponent.Value.Levels[upgradeConfigComponent.Index + 1].MoneyPerUpgrade;

                if (coins < price)
                    continue;

                if (upgradeConfigComponent.Index == upgradeConfigComponent.Value.Levels.Count)
                    continue;

                _wallet.AddDecreaseCoinsEvent(price);
                upgradeConfigComponent.Index++;
                UpdateNextUpgradePriceText(entity);
            }
        }

        private void UpdateNextUpgradePriceText(ProtoEntity entity)
        {
            UpgradeConfigComponent upgradeConfigComponent = entity.GetUpgradeConfig();
            int index = upgradeConfigComponent.Index + 1;
            UpgradeConfig config = upgradeConfigComponent.Value;
            ApplyUpgradeModule module = entity.GetApplyUpgradeUiModule().Value;
            string text = index < config.Levels.Count
                ? config.Levels[index].MoneyPerUpgrade.ToString()
                : "MAX";
            module.NextUpgradePriceText.text = text;

            if (index == config.Levels.Count)
                module.SkullImage.gameObject.SetActive(false);
            
            if (upgradeConfigComponent.Index + 1 == upgradeConfigComponent.Value.Levels.Count)
                module.SkullImage.gameObject.SetActive(false);
        }
    }
}
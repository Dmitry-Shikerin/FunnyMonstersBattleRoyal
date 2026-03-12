using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Components;
using Sources.EcsBoundedContexts.Upgrades.Domain.Components;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine.UI;

namespace Sources.EcsBoundedContexts.Upgrades.Controllers
{
    [EcsSystem(53)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class UpdateAvailabilityUpgradeButtonsSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IEntityRepository _repository;

        [DI] private readonly ProtoIt _upgradeIt = new(
            It.Inc<
                UpgradeConfigComponent,
                ApplyUpgradeUiModuleComponent>());
        [DI] private readonly ProtoIt _walletIt = new(
            It.Inc<
                PlayerWalletComponent,
                CoinsChangedEvent>());
        [DI] private readonly ProtoIt _initIt = new(
            It.Inc<
                UpgradeTag>());

        private ProtoEntity _wallet;

        public UpdateAvailabilityUpgradeButtonsSystem(IEntityRepository repository)
        {
            _repository = repository;
        }

        public void Init(IProtoSystems systems)
        {
            _wallet = _repository.GetByName(IdsConst.PlayerWallet);
            
            foreach (ProtoEntity entity in _initIt)
                UpdateButton(entity);
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _walletIt)
            {
                foreach (ProtoEntity upgradeEntity in _upgradeIt)
                    UpdateButton(upgradeEntity);
            }
        }

        private void UpdateButton(ProtoEntity upgradeEntity)
        {
            int walletValue = _wallet.GetPlayerWallet().Value;
            Button button = upgradeEntity.GetApplyUpgradeUiModule().Value.Button;
            ref UpgradeConfigComponent upgradeConfigComponent = ref upgradeEntity.GetUpgradeConfig();

            if (upgradeConfigComponent.Index == upgradeConfigComponent.Value.Levels.Count - 1)
            {
                button.interactable = false;
                return;
            }

            int nextPrice = upgradeConfigComponent.Value.Levels[upgradeConfigComponent.Index + 1].MoneyPerUpgrade;

            if (walletValue < nextPrice)
            {
                button.interactable = false;
                return;
            }

            button.interactable = true;
        }
    }
}
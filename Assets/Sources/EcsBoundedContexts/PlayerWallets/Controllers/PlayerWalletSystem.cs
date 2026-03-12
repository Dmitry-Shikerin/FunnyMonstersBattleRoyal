using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.PlayerWallets.Domain.Components;
using Sources.EcsBoundedContexts.PlayerWallets.Presentation;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using TMPro;

namespace Sources.EcsBoundedContexts.PlayerWallets.Controllers
{
    [EcsSystem(51)]
    [ComponentGroup(ComponentGroup.Ability)]
    [Aspect(AspectName.Game)]
    public class PlayerWalletSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly IEntityRepository _entityRepository;

        [DI] private readonly ProtoIt _it =
            new(It.Inc<
                PlayerWalletComponent,
                IncreaseCoinsEvent>());
        [DI] private readonly ProtoIt _removeIt =
            new(It.Inc<
                PlayerWalletComponent,
                DecreaseCoinsEvent>());

        [DI] private readonly GameAspect _aspect;

        public PlayerWalletSystem(IEntityRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public void Init(IProtoSystems systems)
        {
            ProtoEntity wallet = _entityRepository.GetByName(IdsConst.PlayerWallet);
            UpdateMoneyTexts(wallet);
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                int addCoins = entity.GetIncreaseCoinsEvent().Value;
                ref PlayerWalletComponent playerWallet = ref entity.GetPlayerWallet();
                playerWallet.Value += addCoins;
                UpdateMoneyTexts(entity);
                
                entity.AddCoinsChangedEvent();
            }
            
            foreach (ProtoEntity entity in _removeIt)
            {
                int removeCoins = entity.GetDecreaseCoinsEvent().Value;
                ref PlayerWalletComponent playerWallet = ref entity.GetPlayerWallet();
                playerWallet.Value -= removeCoins;
                UpdateMoneyTexts(entity);
                
                entity.AddCoinsChangedEvent();
            }
        }

        private void UpdateMoneyTexts(ProtoEntity entity)
        {
            PlayerWalletModule module = entity.GetPlayerWalletModule().Value;
            module.ScullAnimation.Restart();

            foreach (TMP_Text tmpText in module.MoneyTexts)
                tmpText.text = entity.GetPlayerWallet().Value.ToString();
        }
    }
}
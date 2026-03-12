using System;
using System.Net.Sockets;
using System.Threading;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.EcsBoundedContexts.Common.Domain.Constants;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Components;
using Sources.EcsBoundedContexts.DailyRewards.Domain.Configs;
using Sources.EcsBoundedContexts.DailyRewards.Infrastructure;
using Sources.EcsBoundedContexts.DailyRewards.Presentation;
using Sources.Frameworks.GameServices.Loads.Services.Interfaces;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.GameServices.ServerTimes.Services.Interfaces;
using Sources.Frameworks.MyLeoEcsProto.Repositories;
using UnityEngine;

namespace Sources.EcsBoundedContexts.DailyRewards.Controllers
{
    [EcsSystem(56)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.MainMenu, AspectName.Game)]
    public class DailyRewardSystem : IProtoInitSystem, IProtoRunSystem, IProtoDestroySystem
    {
        [DI] private readonly ProtoIt _it = new(
            It.Inc<
            DailyRewardTag,
            ApplyDailyRewardEvent>());

        private readonly IAssetCollector _assetCollector;
        private readonly IEntityRepository _entityRepository;
        private readonly DailyRewardService _dailyRewardService;
        private readonly ITimeService _timeService;
        private readonly IStorageService _storageService;
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(1);
        
        private CancellationTokenSource _tokenSource;
        private ProtoEntity _dailyReward;
        private ProtoEntity _healthBuster;
        private DailyRewardConfig _config;

        public DailyRewardSystem(
            IAssetCollector assetCollector,
            IEntityRepository entityRepository,
            DailyRewardService dailyRewardService,
            ITimeService timeService,
            IStorageService storageService)
        {
            _assetCollector = assetCollector;
            _entityRepository = entityRepository;
            _dailyRewardService = dailyRewardService;
            _timeService = timeService;
            _storageService = storageService;
        }

        public void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<DailyRewardConfig>();
            _dailyReward = _entityRepository.GetByName(IdsConst.DailyReward);
            _healthBuster = _entityRepository.GetByName(IdsConst.HealthBooster);
            StartTimer();
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _it)
            {
                OnClick();
            }
        }

        public void Destroy()
        {
            _tokenSource?.Cancel();
        }

        private async void StartTimer()
        {
            try
            {
                _tokenSource = new CancellationTokenSource();
                DateTime serverTime = _timeService.GetTime();
                _dailyRewardService.SetServerTime(_dailyReward, serverTime);
                
                await UniTask.Delay(_delay, cancellationToken: _tokenSource.Token, ignoreTimeScale: true);
                
                while (_tokenSource.Token.IsCancellationRequested == false)
                {
                    _dailyRewardService.IncreaseServerTime(_dailyReward);
                    _dailyRewardService.SetCurrentTime(_dailyReward);
                    string timerText = _dailyRewardService.GetTimerText(_dailyReward);
                    DailyRewardModule module = _dailyReward.GetDailyRewardModule().Value;
                    module.TimerText.SetText(timerText);
                    ActivateButton(module);
        
                    await UniTask.Delay(_delay, cancellationToken: _tokenSource.Token, ignoreTimeScale: true);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (SocketException)
            {
                _tokenSource.Cancel();
                StartTimer();
            }
        }

        private void OnClick()
        {
            DailyRewardModule module = _dailyReward.GetDailyRewardModule().Value;
            ActivateButton(module);
            
            if (_dailyRewardService.TrySetTargetRewardTime(_dailyReward) == false)
                return;
            
            //module.Animator.Play();
            _healthBuster.AddIncreaseHealthBoosterEvent(_config.HealthBoostersAmount);
            Debug.Log($"IncreaseHealthBoosterEvent");
            _storageService.Save(IdsConst.HealthBooster);
            _storageService.Save(IdsConst.DailyReward);
        }

        private void ActivateButton(DailyRewardModule module)
        {
            if (_dailyRewardService.IsAvailable(_dailyReward) == false)
            {
                module.LockImage.gameObject.SetActive(true);
                module.Button.interactable = false;
                //_view.Button.SetState(UISelectionState.Disabled);
                module.TimerCanvasGroup.alpha = 1;
                module.OutlineImage.gameObject.SetActive(false);
                
                return;
            }
            
            module.LockImage.gameObject.SetActive(false);
            module.Button.interactable = true;
            module.TimerCanvasGroup.alpha = 0;
            module.OutlineImage.gameObject.SetActive(true);
            //_view.Button.SetState(UISelectionState.Normal);
        }
    }
}
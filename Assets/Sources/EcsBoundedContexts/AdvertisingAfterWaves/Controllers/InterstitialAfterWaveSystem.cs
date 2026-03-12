using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Sources.BoundedContexts.Hud.Presentations.Gameplay;
using Sources.EcsBoundedContexts.AdvertisingAfterWaves.Domain;
using Sources.EcsBoundedContexts.Core;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.EcsBoundedContexts.EnemySpawners.Domain.Components;
using Sources.Frameworks.DeepFramework.DeepUiManager.Domain.Enums;
using Sources.Frameworks.DeepFramework.DeepUiManager.Infrastructure.Implementation;
using Sources.Frameworks.GameServices.DeepWrappers.Sounds;
using Sources.Frameworks.GameServices.DeepWrappers.Views.Interfaces;
using Sources.Frameworks.GameServices.Pauses;
using Sources.Frameworks.GameServices.Prefabs.Interfaces;
using Sources.Frameworks.YandexSdkFramework.Sdk.Domain;
using Sources.Frameworks.YandexSdkFramework.Sdk.Services;
using UnityEngine;

namespace Sources.EcsBoundedContexts.AdvertisingAfterWaves.Controllers
{
    [EcsSystem(6)]
    [ComponentGroup(ComponentGroup.Common)]
    [Aspect(AspectName.Game)]
    public class InterstitialAfterWaveSystem : IProtoRunSystem, IProtoInitSystem
    {
        private readonly ISoundService _soundService;
        private readonly IPauseService _pauseService;
        private readonly ISdkService _sdkService;
        private readonly IUiViewService _uiViewService;
        private readonly IAssetCollector _assetCollector;

        [DI] private readonly ProtoIt _spawnerIt = new(
            It.Inc<
                EnemySpawnerTag,
                WaveCompletedEvent>());

        private readonly TimeSpan _timerTimeSpan = TimeSpan.FromSeconds(AdvertisingConst.Delay);
        private CancellationTokenSource _tokenSource;
        private AdvertisingAfterWaveConfig _config;

        public InterstitialAfterWaveSystem(
            ISoundService soundService,
            IPauseService pauseService,
            ISdkService sdkService,
            IUiViewService uiViewService,
            IAssetCollector assetCollector)
        {
            _soundService = soundService;
            _pauseService = pauseService;
            _sdkService = sdkService;
            _uiViewService = uiViewService;
            _assetCollector = assetCollector;
        }

        public void Init(IProtoSystems systems)
        {
            _config = _assetCollector.Get<AdvertisingAfterWaveConfig>();
            _tokenSource = new CancellationTokenSource();
        }

        public void Run()
        {
            foreach (ProtoEntity entity in _spawnerIt)
            {
                int currentWaveNumber = entity.GetEnemySpawnerData().WaweIndex;

                if (currentWaveNumber % _config.WavesCount != 0)
                    continue;

                ShowTimerAsync().Forget();
            }
        }

        private async UniTaskVoid ShowTimerAsync()
        {
            InterstitialAfterWaveUiView view = _uiViewService.Get<InterstitialAfterWaveUiView>();
            _uiViewService.Show(UiViewId.AdvertisingAfterWave);

            Debug.Log($"Show timer");
            if (WebApplication.IsRunningOnWebGL)
            {
                _soundService.PauseMusic();
                _soundService.PauseSounds();
                _pauseService.PauseGame();
            }

            for (int i = _config.SecondsCount; i > 0; i--)
            {
                view.TimerText.text = $"{i}";
                await UniTask.Delay(_timerTimeSpan, ignoreTimeScale: true, cancellationToken: _tokenSource.Token);
            }

            _sdkService.ShowInterstitial();
        }
    }
}